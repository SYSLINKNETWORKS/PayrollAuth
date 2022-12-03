using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using TWP_API_Auth.ViewModels;


namespace TWP_API_Auth.Processor
{
    public class FinancialYearProcessor : IProcessor<FinancialYearBaseModel>
    {
        private DataContext _context;
        private AbsBusiness _AbsBusiness;
        SecurityHelper _SecurityHelper = new SecurityHelper();

        private String _UserName = "";

        public FinancialYearProcessor(DataContext context)
        {
            _context = context;

            _AbsBusiness = Builder.MakeBusinessClass(Enums.ClassName.FinancialYear, _context);
        }
        public async Task<ApiResponse> ProcessGet(Guid _MenuId, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            if (_AbsBusiness != null)
            {
                var response = await _AbsBusiness.GetDataAsync(_User);
                if (Convert.ToInt32(response.statusCode) == 200)
                {
                    apiResponse = await _SecurityHelper.UserMenuPermissionAsync(_MenuId, _User);
                    if (apiResponse.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponse; }
                    var _UserMenuPermissionAsync = (GetUserPermissionViewModel)apiResponse.data;

                    if (!_UserMenuPermissionAsync.View_Permission)
                    {
                        apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                        return apiResponse;
                    }

                    var _Table = (IEnumerable<FinancialYear>)response.data;
                    var result = (from ViewTable in _Table
                                  select new FinancialYearViewModel
                                  {
                                      Id = ViewTable.Id,
                                      Name = ViewTable.StartYear.ToString() + "/" + ViewTable.EndYear.ToString(),
                                      StartDate = ViewTable.StartDate,
                                      EndDate = ViewTable.EndDate,
                                      Type = ViewTable.Type,
                                      Active = ViewTable.Active,
                                      NewPermission = _UserMenuPermissionAsync.Insert_Permission,
                                      UpdatePermission = _UserMenuPermissionAsync.Update_Permission,
                                      DeletePermission = _UserMenuPermissionAsync.Delete_Permission,
                                  }).ToList();
                    response.data = result;

                }
                return response;
            }
            return null;

        }

        public async Task<ApiResponse> ProcessGetById(Guid _Id, Guid _MenuId, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            if (_AbsBusiness != null)
            {
                apiResponse = await _SecurityHelper.UserMenuPermissionAsync(_MenuId, _User);
                if (apiResponse.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponse; }
                var _UserMenuPermissionAsync = (GetUserPermissionViewModel)apiResponse.data;

                if (!_UserMenuPermissionAsync.Update_Permission)
                {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                    return apiResponse;
                }

                var response = await _AbsBusiness.GetDataByIdAsync(_Id, _User);
                if (Convert.ToInt32(response.statusCode) == 200)
                {
                    var _Table = (FinancialYear)response.data;
                    var _ViewModel = new FinancialYearViewByIdModel
                    {
                        Id = _Table.Id,
                        StartDate = _Table.StartDate,
                        EndDate = _Table.EndDate,
                        Type = _Table.Type,
                        Active = _Table.Active,

                    };
                    response.data = _ViewModel;
                }
                return response;
            }
            return null;
        }

        public async Task<ApiResponse> ProcessPost(object request, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            var _request = (FinancialYearAddModel)request;

            _UserName = _User.Claims.FirstOrDefault(rec => rec.Type == Enums.Misc.UserName.ToString())?.Value.ToString();

            if (_AbsBusiness != null)
            {
                apiResponse = await _SecurityHelper.UserMenuPermissionAsync(_request.Menu_Id, _User);
                if (apiResponse.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponse; }
                var _UserMenuPermissionAsync = (GetUserPermissionViewModel)apiResponse.data;

                if (!_UserMenuPermissionAsync.Insert_Permission)
                {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                    return apiResponse;
                }
                string error = "";
                bool _NameExists = _context.FinancialYears.Any(rec => rec.StartDate.Equals(_request.StartDate) && rec.Action != Enums.Operations.D.ToString());
                if (_NameExists)
                {
                    error = error + "Name";
                }
                if (_NameExists)
                {
                    apiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                    apiResponse.message = error + " Already Exist";
                    return apiResponse;
                }


                var _Table = new FinancialYear
                {
                    Action = Enums.Operations.A.ToString(),
                    CompanyId = _UserMenuPermissionAsync.CompanyId,
                    StartDate = _request.StartDate,
                    StartDay = _request.StartDate.Day,
                    StartMonth = _request.StartDate.Month,
                    StartYear = _request.StartDate.Year,
                    EndDate = _request.EndDate,
                    EndDay = _request.EndDate.Day,
                    EndMonth = _request.EndDate.Month,
                    EndYear = _request.EndDate.Year,
                    Active = _request.Active,
                    Type = Enums.ColumnType.U.ToString(),
                    UserNameInsert = _UserName,
                    InsertDate = DateTime.Now,
                };
                return await _AbsBusiness.AddAsync(_Table, _User);
            }
            return null;
        }

        public async Task<ApiResponse> ProcessPut(object request, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            _UserName = _User.Claims.FirstOrDefault(rec => rec.Type == Enums.Misc.UserName.ToString())?.Value.ToString();
            var _request = (FinancialYearUpdateModel)request;
            if (_AbsBusiness != null)
            {
                apiResponse = await _SecurityHelper.UserMenuPermissionAsync(_request.Menu_Id, _User);
                if (apiResponse.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponse; }
                var _UserMenuPermissionAsync = (GetUserPermissionViewModel)apiResponse.data;

                if (!_UserMenuPermissionAsync.Update_Permission)
                {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                    return apiResponse;
                }

                string error = "";
                bool _NameExists = _context.FinancialYears.Any(rec => rec.StartDate.Equals(_request.StartDate) && rec.Id != _request.Id && rec.Action != Enums.Operations.D.ToString());

                if (_NameExists)
                {
                    error = error + "Name";
                }

                if (_NameExists)
                {
                    apiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                    apiResponse.message = error + " Already Exist";
                    return apiResponse;
                }


                var result = _context.FinancialYears.Where(a => a.Id == _request.Id && a.Action != Enums.Operations.D.ToString()).FirstOrDefault();
                if (result == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found ";
                    return apiResponse;
                }
                result.Action = Enums.Operations.E.ToString();
                result.CompanyId = _UserMenuPermissionAsync.CompanyId;
                result.StartDate = _request.StartDate;
                result.StartDay = _request.StartDate.Day;
                result.StartMonth = _request.StartDate.Month;
                result.StartYear = _request.StartDate.Year;
                result.EndDate = _request.EndDate;
                result.EndDay = _request.EndDate.Day;
                result.EndMonth = _request.EndDate.Month;
                result.EndYear = _request.EndDate.Year;
                result.Active = _request.Active;
                result.Type = Enums.ColumnType.U.ToString();
                result.UserNameUpdate = _UserName;
                result.UpdateDate = DateTime.Now;
                return await _AbsBusiness.UpdateAsync(result, _User);
            }
            return null;

        }

        public async Task<ApiResponse> ProcessDelete(object request, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            if (_AbsBusiness != null)
            {
                _UserName = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserName.ToString())?.Value.ToString();

                var _request = (FinancialYearDeleteModel)request;

                Guid _Id = _request.Id;
                Guid _MenuId = _request.Menu_Id;

                apiResponse = await _SecurityHelper.UserMenuPermissionAsync(_MenuId, _User);
                if (apiResponse.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponse; }
                var _UserMenuPermissionAsync = (GetUserPermissionViewModel)apiResponse.data;

                if (!_UserMenuPermissionAsync.Delete_Permission)
                {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                    return apiResponse;
                }


                return await _AbsBusiness.DeleteAsync(_Id, _User);
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }

    }
}
