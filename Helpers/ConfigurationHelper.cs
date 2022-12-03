using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace TWP_API_Auth.Helpers
{
    public class ConfigurationHelper
    {
        //Encrypt Password Start
        string _connectionString = "";
        DataContext _context;
        public ConfigurationHelper()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            _connectionString = configuration.GetConnectionString("Connection").ToString();
            var options = new DbContextOptionsBuilder<DataContext>().UseSqlServer(_connectionString).Options;

            _context = new DataContext(options);

        }



        //Menu Permission
        public async Task<ApiResponse> FinancialYearAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            var _FinancialYearsTable = await _context.FinancialYears.ToListAsync();
            _FinancialYearsTable.ForEach(a => a.Active = false);
            _context.UpdateRange(_FinancialYearsTable);
            await _context.SaveChangesAsync();


            apiResponse.statusCode = StatusCodes.Status200OK.ToString();
            return apiResponse;
        }


    }
}