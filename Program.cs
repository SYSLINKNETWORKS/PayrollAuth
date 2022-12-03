
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using TWP_API_Auth.Processor;
using TWP_API_Auth.Processor.Process;
using TWP_API_Auth.Repository;
using TWP_API_Auth.Services;
using TWP_API_Auth.ViewModels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddControllers();

builder.Services.AddApiVersioning(config =>
           {
               config.DefaultApiVersion = new ApiVersion(1, 0);
               config.AssumeDefaultVersionWhenUnspecified = true;
               config.ReportApiVersions = true;
           });

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 3;
                options.Lockout.MaxFailedAccessAttempts = 5;

            }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();



builder.Services.AddAuthentication(auth =>
           {
               auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = false,
                       ValidateIssuerSigningKey = true,
                       RequireExpirationTime = true,
                       ValidIssuer = builder.Configuration["AuthSettings:Issuer"], //some string, normally web url,
                                                                                   //ValidAudience = Configuration["AuthSettings:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"]))
                   };

                   options.Events = new JwtBearerEvents
                   {
                       OnTokenValidated = context =>
                       {
                           string _TokenString = context.HttpContext.Request.Headers["Authorization"].ToString();
                           var _Token = _TokenString.Substring(7, _TokenString.Length - 7);
                           var _Handler = new JwtSecurityTokenHandler();
                           var _TokenDecode = _Handler.ReadJwtToken(_Token);
                           string _Key = _TokenDecode.Audiences.ToList()[0].ToString();
                           SecurityHelper _SecurityHelper = new SecurityHelper();
                           var _result = _SecurityHelper.KeyValidation(_Key).GetAwaiter().GetResult();
                           if (_result.statusCode != StatusCodes.Status200OK.ToString()) { context.Fail("Unauthorized"); }
                           return Task.CompletedTask;
                       },
                       OnAuthenticationFailed = context =>
                       {
                           if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                           {
                               context.Response.Headers.Add("Token-Expired", "true");
                           }
                           return Task.CompletedTask;
                       }
                   };
               });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TWP API Auth", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });



//Auth
builder.Services.AddTransient<IMailService, SendGridMailService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProcessor<ClaimBaseModel>, AuthClaimProcessor>();
builder.Services.AddScoped<IProcessor<RoleBaseModel>, RoleProcessor>();
builder.Services.AddScoped<IProcessor<UserClaimsViewModel>, UserClaimProcessor>();
builder.Services.AddScoped<IProcessor<UserRolesViewModel>, UserRoleProcessor>();
builder.Services.AddScoped<IProcessor<UserBaseModel>, UsersProcessor>();
builder.Services.AddScoped<IProcessor<UserMenuBaseModel>, UserMenuProcessor>();
builder.Services.AddScoped<IProcessor<UserRolePermissionBaseModel>, UserRolePermissionProcessor>();
builder.Services.AddScoped<IProcessor<GetMenuBaseModel>, GetMenuProcessor>();
builder.Services.AddScoped<IProcessor<UserMenuModuleBaseModel>, UserMenuModuleProcessor>();
builder.Services.AddScoped<IProcessor<UserMenuCategoryBaseModel>, UserMenuCategoryProcessor>();
builder.Services.AddScoped<IProcessor<UserMenuSubCategoryBaseModel>, UserMenuSubCategoryProcessor>();

//Configuration
builder.Services.AddScoped<IProcessor<CompanyBaseModel>, CompanyProcessor>();
builder.Services.AddScoped<IProcessor<BranchBaseModel>, BranchProcessor>();
builder.Services.AddScoped<IProcessor<FinancialYearBaseModel>, FinancialYearProcessor>();

//Lov
builder.Services.AddScoped<ICfLovServicesRepository, CfLovServicesRepository>();

//Backup
builder.Services.AddScoped<IBackupSevicesRepository, BackupSevicesRepository>();

//Version
builder.Services.AddScoped<IVersionSevicesRepository, VersionSevicesRepository>();

//UserPermission
builder.Services.AddScoped<IMicroservices, Microservices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

//app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
