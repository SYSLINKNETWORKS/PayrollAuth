using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;

namespace TWP_API_Auth.Bussiness
{
    public class BFinancialYear : AbsBusiness
    {
        private readonly DataContext _context;
        ErrorLog _ErrorLog = new ErrorLog();
        //        String _UserId = "";
        String _UserName = "";

        public BFinancialYear(DataContext context)
        {
            _context = context;
        }

        public override async Task<ApiResponse> GetDataAsync(ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {
                var _Table = await _context.FinancialYears.Where(c => c.Action != Enums.Operations.D.ToString()).ToListAsync();

                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record Not Found";
                    return ApiResponse;
                }
                if (_Table.Count == 0)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record Not Found";
                    return ApiResponse;
                }
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _Table;
                return ApiResponse;
            }
            catch (DbUpdateException e)
            {
                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = _ErrorId;
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

        public override async Task<ApiResponse> GetDataByIdAsync(Guid Id, ClaimsPrincipal _User)
        {
            ApiResponse ApiResponse = new ApiResponse();
            try
            {
                var _Table = await _context.FinancialYears.Where(x => x.Id == Id && x.Action != Enums.Operations.D.ToString()).FirstOrDefaultAsync();
                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record Not Found";
                    return ApiResponse;
                }
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _Table;
                return ApiResponse;
            }
            catch (DbUpdateException e)
            {
                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = _ErrorId;
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
        public override async Task<ApiResponse> AddAsync(object model, ClaimsPrincipal _User)
        {
            ApiResponse ApiResponse = new ApiResponse();
            try
            {
                var _model = (FinancialYear)model;
                if (_model.Active)
                {
                    ConfigurationHelper _ConfigurationHelper = new ConfigurationHelper();
                    await _ConfigurationHelper.FinancialYearAsync();
                }


                await _context.FinancialYears.AddAsync(_model);
                _context.SaveChanges();

                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.message = "Record Save : " + _model.StartYear.ToString() + "/" + _model.EndYear.ToString();
                return ApiResponse;

            }
            catch (DbUpdateException e)
            {
                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = _ErrorId;
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
                _UserName = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserName.ToString())?.Value.ToString();
                var _model = (FinancialYear)model;

                if (_model.Active)
                {
                    ConfigurationHelper _ConfigurationHelper = new ConfigurationHelper();
                    await _ConfigurationHelper.FinancialYearAsync();
                }


                _context.FinancialYears.Update(_model);
                await _context.SaveChangesAsync();

                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.message = "Record Update : " + _model.StartYear.ToString() + "/" + _model.EndYear.ToString();
                return ApiResponse;

            }
            catch (DbUpdateException e)
            {
                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = _ErrorId;
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
                _UserName = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserName.ToString())?.Value.ToString();
                var _Table = _context.FinancialYears.Where(a => a.Id == _Id && a.Action != Enums.Operations.D.ToString()).FirstOrDefault();
                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found ";
                    return ApiResponse;
                }
                if (_Table.Active)
                {
                    ApiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                    ApiResponse.message = "Active financial year cannot be delete ";
                    return ApiResponse;

                }

                _Table.Action = Enums.Operations.D.ToString();
                _Table.UserNameDelete = _UserName;
                _Table.DeleteDate = DateTime.Now;

                _context.Update(_Table);
                await _context.SaveChangesAsync();
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.message = "Record Delete : " + _Table.StartYear.ToString() + "/" + _Table.EndYear.ToString();
                return ApiResponse;

            }
            catch (DbUpdateException e)
            {
                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = _ErrorId;
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