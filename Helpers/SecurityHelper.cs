using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
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
    public class SecurityHelper
    {
        //Encrypt Password Start
        string _connectionString = "";
        public SecurityHelper()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            _connectionString = configuration.GetConnectionString("Connection").ToString();

        }
        public string security(string txtbox)
        {
            string secpwd = "";
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            Byte[] data = System.Text.ASCIIEncoding.Default.GetBytes(txtbox.ToString().Trim());
            secpwd = BitConverter.ToString(sha.ComputeHash(data));
            return secpwd.Substring(0, 50);
        }
        //Encrypt Password End
        public async Task<ApiResponse> UserMenuPermissionAsync(Guid _MenuId, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            string _Key = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.Key.ToString())?.Value.ToString();

            var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false)
        .Build();

            HttpClient client = new HttpClient();
            var _BaseUri = configuration["ConnectionStrings:GWAPI"] + "/Auth/v1/";
            client.BaseAddress = new Uri(_BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("MenuId", _MenuId.ToString());
            client.DefaultRequestHeaders.Add("Key", _Key);// _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.Key.ToString())?.Value.ToString());
            var url = "Microservices/MenuPermission";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var _JsonData = response.Content.ReadAsStringAsync().Result;
                if (_JsonData != null)
                {
                    GetUserPermissionViewModel _GetUserPermissionViewModel = new GetUserPermissionViewModel();

                    ApiResponse _ApiResponse = JsonConvert.DeserializeObject<ApiResponse>(_JsonData.ToString());
                    var entities = JObject.Parse(_JsonData).SelectToken("data");
                    _GetUserPermissionViewModel = JsonConvert.DeserializeObject<GetUserPermissionViewModel>(entities.ToString());
                    _ApiResponse.data = _GetUserPermissionViewModel;
                    apiResponse = _ApiResponse;
                }
                return apiResponse;
            }
            apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
            apiResponse.message = "Invalid Permission";
            return apiResponse;
        }

        //Menu Permission
        public ApiResponse UserMenuPermission(Guid _MenuId, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();

            string _Key = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.Key.ToString())?.Value.ToString();
            //            string _UserId = _User.Claims.FirstOrDefault (c => c.Type == Enums.Misc.UserId.ToString ())?.Value.ToString ();

            var options = new DbContextOptionsBuilder<DataContext>().UseSqlServer(_connectionString).Options;
            using (var ctx = new DataContext(options))
            {
                var _Permission = from _UserKey in ctx.UserLoginAudits
                                  join _Users in ctx.Users on _UserKey.UserId equals _Users.Id
                                  join _UserRoles in ctx.UserRoles on _Users.Id equals _UserRoles.UserId
                                  join _UserRolePermission in ctx.UserRolePermissions on _UserRoles.RoleId equals _UserRolePermission.Roles_Id
                                  join _UserMenu in ctx.UserMenus on _UserRolePermission.Menu_Id equals _UserMenu.Id

                                  where _UserKey.Key == _Key && _UserRolePermission.Menu_Id == _MenuId
                                  select new GetUserPermissionViewModel
                                  {
                                      MenuId = _UserRolePermission.Menu_Id,
                                      MenuName = _UserMenu.Name,
                                      MenuAlias = _UserMenu.Alias,
                                      CompanyId = _UserKey.CompanyId,
                                      BranchId = _UserKey.BranchId,
                                      FinancialYearId = _UserKey.YearId.Value,
                                      View_Permission = _UserRolePermission.View_Permission,
                                      Insert_Permission = _UserRolePermission.Insert_Permission,
                                      Update_Permission = _UserRolePermission.Update_Permission,
                                      Delete_Permission = _UserRolePermission.Delete_Permission,
                                      Check_Permission = _UserRolePermission.Check_Permission,
                                      Approve_Permission = _UserRolePermission.Approve_Permission,

                                  };
                if (_Permission == null)
                {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                    apiResponse.message = "Invalid Permission";
                    return apiResponse;
                }

                if (_Permission.Count() == 0)
                {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                    apiResponse.message = "Invalid Permission";
                    return apiResponse;
                }
                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.data = _Permission.FirstOrDefault();
                return apiResponse;
                //                return _Permission.FirstOrDefault ();
            }
        }
        public static string GetReferencialKey(string Salt, string Key, string Prefix)
        {
            /*
            1. Decript Salt with Key
            2. var DecryptedSalt=Decript Salt with Key 
            3. var middleString= Prefix.SubString(1,Prefix.Length-2)
            4. string Text=Prefix.SubString(0,1)+DecriptedSalt+middlestring+DecriptedSalt+prefix.substring(Prefix.Length-2,1)
            5. hash Text
            6. return Text
            */
            // Generate new Guid.
            //var result = Guid.NewGuid();

            // if (Salt.Trim() != "" && Salt.Trim() != null && Key.Trim() != "" && Key.Trim() != null && Prefix.Trim() != "" && Prefix.Trim() != null)
            // {
            var DecryptedSalt = Salt; // DecryptString(Key, Salt);
            var middleString = Prefix.Trim().Substring(1, Prefix.Length - 2);
            var chars = Prefix.Trim().Substring(0, 1) + DecryptedSalt + middleString + DecryptedSalt + Prefix.Trim().Substring(Prefix.Length - 2, 1);
            //}

            return chars.ToString().Substring(0, 16);

        }

        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            //byte[] iv = new byte[key.Length];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            //byte[] iv = new byte[key.Length];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static string GetRandomKey(int length)
        {
            // // Generate new Guid for key.
            // var randomKey = Guid.NewGuid().ToString("N");
            // var key = (from t in randomKey where char.IsDigit(t) select t).ToString().Substring(0, 4);

            // return key;
            Random random = new Random();
            var chars = "0123456789012345678901234567890123456789";
            return new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(length).ToArray());

        }

        public static string GetRandomSalt(int length)
        {

            // var randomSalt = new Guid(Guid.NewGuid().ToByteArray()
            //         .Select(b => (byte)(((b % 16) < 10 ? 0xA : b) |
            //                             (((b >> 4) < 10 ? 0xA : (b >> 4)) << 4)))
            //         .ToArray()).ToString().Substring(0, 4);

            // Random random = new Random();
            // var chars = "abcdefghijklmnopqrstuvwxyz";
            // return new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(36).ToArray());
            // //    return randomSalt;

            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            return str_build.ToString();
        }

        public static Dictionary<string, string> Addfk(string Prefix, Guid Fk_Id)
        {
            var _SecurityDictionary = new Dictionary<string, string>();
            // Generate new Guid for salt.

            var _randomSalt = GetRandomSalt(16);

            // Generate new Guid for key.
            var _randomKey = GetRandomKey(16);

            // get 4 char prefix.
            var _prefix = Prefix.Trim().Substring(0, 4);

            var _encryptSalt = SecurityHelper.EncryptString(_randomKey, _randomSalt);

            var _KeySalt = GetReferencialKey(_randomSalt, _randomKey, _prefix);

            var _encryptFkId = EncryptString(_KeySalt, Fk_Id.ToString());

            _SecurityDictionary.Add("Key", _randomKey);
            _SecurityDictionary.Add("Prefix", _prefix);
            _SecurityDictionary.Add("Salt", _encryptSalt);
            _SecurityDictionary.Add("FkId", _encryptFkId);
            return _SecurityDictionary;

        }
        public static Guid Dfk(string _Salt, string _Key, string _Prefix, string _FKID)
        {
            var _decryptSalt = SecurityHelper.DecryptString(_Key, _Salt);
            var _KeySalt = SecurityHelper.GetReferencialKey(_decryptSalt, _Key, _Prefix);
            var _decryptFkId = SecurityHelper.DecryptString(_KeySalt, _FKID.ToString().Trim());

            return Guid.Parse(_decryptFkId); ;
        }

        //Generate Password Start

        public static string PasswordGenerate()
        {
            PasswordGenerator pg = new PasswordGenerator();
            string password = pg.Generate();
            return password;
        }
        public async Task<ApiResponse> KeyValidation(string _Key)
        {
            ApiResponse apiResponse = new ApiResponse();
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", false)
           .Build();
            HttpClient client = new HttpClient();
            var _BaseUri = configuration["ConnectionStrings:GWAPI"] + "/Auth/v1/";
            client.BaseAddress = new Uri(_BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Key", _Key);
            var url = "Microservices/UserKeyVerification";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var _JsonData = response.Content.ReadAsStringAsync().Result;
                if (_JsonData != null)
                {
                    ApiResponse _ApiResponse = JsonConvert.DeserializeObject<ApiResponse>(_JsonData.ToString());
                    apiResponse = _ApiResponse;
                }
                return apiResponse;
            }
            apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
            apiResponse.message = "Record not found";
            return apiResponse;
        }
        public async Task<ApiResponse> GetEmployeesAsync(string _Key)
        {
            ApiResponse apiResponse = new ApiResponse();

            var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false)
                        .Build();

            HttpClient client = new HttpClient();
            var _BaseUri = configuration["ConnectionStrings:GWAPI"] + "/Payroll/v1/";
            client.BaseAddress = new Uri(_BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Add("Key", _Key);
            var url = "Microservices/MSEmployees";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var _JsonData = response.Content.ReadAsStringAsync().Result;
                if (_JsonData != null)
                {
                    ApiResponse _ApiResponse = JsonConvert.DeserializeObject<ApiResponse>(_JsonData.ToString());
                    _ApiResponse.data = JsonConvert.DeserializeObject<List<EmployeesMicroServiceViewModel>>(_ApiResponse.data.ToString());
                    apiResponse = _ApiResponse;

                }
                return apiResponse;
            }
            apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
            apiResponse.message = "Record not found";
            return apiResponse;

        }
        public async Task<ApiResponse> GetEmployeesForLoginAsync(string _TokenString)
        {
            var _Token = _TokenString.Substring(7, _TokenString.Length - 7);
            var _Handler = new JwtSecurityTokenHandler();
            var _TokenDecode = _Handler.ReadJwtToken(_Token);
            string _Key = _TokenDecode.Audiences.ToList()[0].ToString();


            ApiResponse apiResponse = new ApiResponse();

            var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false)
                        .Build();

            HttpClient client = new HttpClient();

            var _BaseUri = configuration["ConnectionStrings:GWAPI"] + "/Payroll/v1/";
            client.BaseAddress = new Uri(_BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Add("Authorization", _TokenString);
            client.DefaultRequestHeaders.Add("Key", _Key);

            var url = "Microservices/MSEmployees";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var _JsonData = response.Content.ReadAsStringAsync().Result;
                if (_JsonData != null)
                {
                    ApiResponse _ApiResponse = JsonConvert.DeserializeObject<ApiResponse>(_JsonData.ToString());
                    _ApiResponse.data = JsonConvert.DeserializeObject<List<EmployeesMicroServiceViewModel>>(_ApiResponse.data.ToString());
                    apiResponse = _ApiResponse;

                }
                return apiResponse;
            }
            apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
            apiResponse.message = "Record not found";
            return apiResponse;

        }
        public async Task<ApiResponse> GetEmployeeByIdAsync(string _Key, Guid _Id)
        {
            ApiResponse apiResponse = new ApiResponse();

            var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false)
                        .Build();

            HttpClient client = new HttpClient();
            var _BaseUri = configuration["ConnectionStrings:GWAPI"] + "/Payroll/v1/";
            client.BaseAddress = new Uri(_BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Add("Key", _Key);
            client.DefaultRequestHeaders.Add("_Id", _Id.ToString());
            var url = "Microservices/MSEmployeeById";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var _JsonData = response.Content.ReadAsStringAsync().Result;
                if (_JsonData != null)
                {
                    ApiResponse _ApiResponse = JsonConvert.DeserializeObject<ApiResponse>(_JsonData.ToString());
                    _ApiResponse.data = JsonConvert.DeserializeObject<EmployeeByIdMicroServiceViewModel>(_ApiResponse.data.ToString());
                    apiResponse = _ApiResponse;

                }
                return apiResponse;
            }
            apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
            apiResponse.message = "Record not found";
            return apiResponse;

        }

        public async Task<ApiResponse> GetItemCategoryAsync(string _Key)
        {
            ApiResponse apiResponse = new ApiResponse();

            var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false)
                        .Build();

            HttpClient client = new HttpClient();
            var _BaseUri = configuration["ConnectionStrings:GWAPI"] + "/Inventory/v1/";
            client.BaseAddress = new Uri(_BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Add("Key", _Key);
            var url = "Microservices/MSItemCategory";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var _JsonData = response.Content.ReadAsStringAsync().Result;
                if (_JsonData != null)
                {
                    ApiResponse _ApiResponse = JsonConvert.DeserializeObject<ApiResponse>(_JsonData.ToString());
                    _ApiResponse.data = JsonConvert.DeserializeObject<List<MSItemCategoryViewModel>>(_ApiResponse.data.ToString());
                    apiResponse = _ApiResponse;

                }
                return apiResponse;
            }
            apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
            apiResponse.message = "Record not found";
            return apiResponse;

        }

        public async Task<ApiResponse> LoginAttendanceAsync(Guid _EmployeeId)
        {
            ApiResponse apiResponse = new ApiResponse();

            var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false)
        .Build();
            var Passphase = configuration["AuthSettings:Key"];

            HttpClient client = new HttpClient();
            var _BaseUri = configuration["ConnectionStrings:GWAPI"] + "/Payroll/v1/";
            client.BaseAddress = new Uri(_BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Passphase", Passphase);// _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.Key.ToString())?.Value.ToString());
            client.DefaultRequestHeaders.Add("EmployeeId", _EmployeeId.ToString());
            client.DefaultRequestHeaders.Add("LoginDate", DateTime.Today.ToString("yyyy-MM-dd"));
            var url = "Microservices/MSLoginAttendance";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var _JsonData = response.Content.ReadAsStringAsync().Result;
                if (_JsonData != null)
                {
                     apiResponse = JsonConvert.DeserializeObject<ApiResponse>(_JsonData.ToString());
                }
                return apiResponse;
            }
            apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
            apiResponse.message = "MSLoginAttendance Not Working";
            return apiResponse;
        }

    }
}