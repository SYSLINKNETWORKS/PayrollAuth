using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TWP_API_Auth.Bussiness
{
    public class BUserMenuModule : AbsBusiness
    {
        private readonly DataContext _context;
        private ErrorLog _ErrorLog = new ErrorLog();

        String _UserId = "";
        public BUserMenuModule(DataContext context)
        {
            _context = context;
        }

        public override async Task<ApiResponse> GetDataAsync(ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {
                var _Table = await _context.UserMenuModules.Where(a => a.Action != Enums.Operations.D.ToString()).ToListAsync();

                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString(); ;
                    ApiResponse.message = "Record Not Found";
                    return ApiResponse;
                }

                if (_Table.Count == 0)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString(); // "401";
                    ApiResponse.message = "Record Not Found";
                    return ApiResponse;
                }
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _Table;
                return ApiResponse;

            }
            catch (Exception e)
            {

                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = e.Message.ToString() + innerexp;
                return ApiResponse;
            }
        }
        public override async Task<ApiResponse> GetDataByIdAsync(Guid _Id, ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {
                var _Table = await _context.UserMenuModules.Where(a => a.Id == _Id && a.Action != Enums.Operations.D.ToString()).FirstOrDefaultAsync();

                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _Table;
                return ApiResponse;
            }
            catch (Exception e)
            {

                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = e.Message.ToString() + innerexp;
                return ApiResponse;
            }
        }
        public override async Task<ApiResponse> AddAsync(object model, ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {

                _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
                var _model = (UserMenuModule)model;

                string error = "";
                bool _NameExists = _context.UserMenuModules.Any(rec => rec.Name.Trim().ToLower().Equals(_model.Name.Trim().ToLower()) && rec.CompanyId.Equals(_model.CompanyId) && rec.Action != Enums.Operations.D.ToString());

                if (_NameExists)
                {
                    error = error + "Name";
                }

                if (_NameExists)
                {
                    ApiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                    ApiResponse.message = error + " Already Exist";
                    return ApiResponse;
                }

                _model.UserIdInsert = _UserId;
                _model.InsertDate = DateTime.Now;
                _model.Action = Enums.Operations.A.ToString();

                await _context.UserMenuModules.AddAsync(_model);
                _context.SaveChanges();


                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.message = "Record Save : " + _model.Name;
                return ApiResponse;

            }
            catch (Exception e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = _ErrorId;
                return ApiResponse;

            }
        }
        public override async Task<ApiResponse> UpdateAsync(object model, ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {
                _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
                var _model = (UserMenuModule)model;

                string error = "";
                bool _NameExists = _context.UserMenuModules.Any(rec => rec.Name.Trim().ToLower().Equals(_model.Name.Trim().ToLower()) && rec.CompanyId == _model.CompanyId && rec.Action != Enums.Operations.D.ToString());


                if (_NameExists)
                {
                    error = error + "Name";
                }

                if (_NameExists)
                {
                    ApiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                    ApiResponse.message = error + " Already Exist";
                    return ApiResponse;
                }

                var result = _context.UserMenuModules.Where(a => a.Id == _model.Id && a.Action != Enums.Operations.D.ToString()).FirstOrDefault();
                if (result == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found ";
                    return ApiResponse;
                }

                result.Name = _model.Name;
                result.Icon = _model.Icon;
                result.Type = _model.Type;
                result.Active = _model.Active;
                result.UserIdUpdate = _UserId;
                result.Action = Enums.Operations.E.ToString();
                result.UserIdUpdate = _UserId;
                result.UpdateDate = DateTime.Now;

                await _context.SaveChangesAsync();


                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.message = "Record Update : " + result.Name;
                return ApiResponse;

            }
            catch (Exception e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = _ErrorId;
                return ApiResponse;

            }
        }
        public override async Task<ApiResponse> DeleteAsync(Guid _Id, ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {

                _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
                var _Table = _context.UserMenuModules.Where(a => a.Id == _Id && a.Action != Enums.Operations.D.ToString()).FirstOrDefault();
                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found ";
                    return ApiResponse;
                }

                _Table.Action = Enums.Operations.D.ToString();
                _Table.UserIdDelete = _UserId;
                _Table.DeleteDate = DateTime.Now;


                await _context.SaveChangesAsync();
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.message = "Record Delete : " + _Table.Name;
                return ApiResponse;

            }
            catch (Exception e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = _ErrorId;
                return ApiResponse;


            }
        }
    }
}