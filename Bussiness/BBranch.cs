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

namespace TWP_API_Auth.Bussiness
{
    public class BBranch : AbsBusiness
    {
        private readonly DataContext _context;
        String _UserId = "";
        Guid _CompanyId;


        public BBranch(DataContext context)
        {
            _context = context;
        }

        public override async Task<ApiResponse> GetDataAsync(ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {
                _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
                _CompanyId = (await _context.Users.Where(s => s.Id == _UserId).Include(b => b.Branches).FirstOrDefaultAsync()).Branches.CompanyId;

                var _Table = await _context.Branches.Include(c => c.Companies).Where(a => a.Action != Enums.Operations.D.ToString() && a.CompanyId == _CompanyId).ToListAsync();

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
                _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
                _CompanyId = (await _context.Users.Where(s => s.Id == _UserId).Include(b => b.Branches).FirstOrDefaultAsync()).Branches.CompanyId;

                var _Table = await _context.Branches.Include(c => c.Companies).Where(a => a.Id == _Id && a.Action != Enums.Operations.D.ToString() && a.CompanyId == _CompanyId).FirstOrDefaultAsync();

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
                var _model = (Branch)model;



                await _context.Branches.AddAsync(_model);

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
                var _model = (Branch)model;


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
            var ApiResponse = new ApiResponse();
            try
            {

                string _UserName = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserName.ToString())?.Value.ToString();

                var _Table = _context.Branches.Where(a => a.Id == _Id && a.Action != Enums.Operations.D.ToString()).FirstOrDefault();
                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found ";
                    return ApiResponse;
                }

                _Table.Action = Enums.Operations.D.ToString();
                _Table.UserNameDelete = _UserId;
                _Table.DeleteDate = DateTime.Now;

                List<AuthClaim> authClaims = _context.AuthClaims.Where(a => a.Menu_Id == _Id).ToList();
                if (authClaims != null && authClaims.Count > 0)
                {
                    foreach (var item in authClaims)
                    {
                        _context.AuthClaims.Remove(item);
                    }
                }

                await _context.SaveChangesAsync();
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.message = "Record Delete : " + _Table.Name;
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
    }
}