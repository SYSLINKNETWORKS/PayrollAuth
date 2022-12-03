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
    public class BCompany : AbsBusiness
    {
        private readonly DataContext _context;

        public BCompany(DataContext context)
        {
            _context = context;
        }

        public override async Task<ApiResponse> GetDataAsync(ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {
                var _Table = await _context.Companies.Where(a => a.Action != Enums.Operations.D.ToString()).ToListAsync();

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
                var _Table = await _context.Companies.Where(a => a.Id == _Id && a.Action != Enums.Operations.D.ToString()).FirstOrDefaultAsync();

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

                var _model = (Company)model;

                await _context.Companies.AddAsync(_model);
                _context.SaveChanges();


                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.message = "Record Save : " + _model.Name;
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
        public override async Task<ApiResponse> UpdateAsync(object model, ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {
                var _model = (Company)model;


                _context.Update(_model);
                await _context.SaveChangesAsync();

                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.message = "Record Update : " + _model.Name;
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
        public override async Task<ApiResponse> DeleteAsync(Guid _Id, ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse();
            try
            {
                string _UserName = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserName.ToString())?.Value.ToString();

                var _Table = _context.Companies.Where(a => a.Id == _Id && a.Action != Enums.Operations.D.ToString()).FirstOrDefault();
                if (_Table == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found ";
                    return apiResponse;
                }

                _Table.Action = Enums.Operations.D.ToString();
                _Table.UserNameDelete = _UserName;
                _Table.DeleteDate = DateTime.Now;

                _context.Update(_Table);
                await _context.SaveChangesAsync();
                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.message = "Record Delete : " + _Table.Name;
                return apiResponse;

            }
            catch (Exception e)
            {

                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = e.Message.ToString() + innerexp;
                return apiResponse;
            }
        }
    }
}