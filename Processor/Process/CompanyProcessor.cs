using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Processor
{
    public class CompanyProcessor : IProcessor<CompanyBaseModel>
    {
        private DataContext _context;
        private AbsBusiness _AbsBusiness;

        private SecurityHelper _SecurityHelper = new SecurityHelper();
        public CompanyProcessor(App_Data.DataContext context)
        {
            _context = context;

            _AbsBusiness = Builder.MakeBusinessClass(Enums.ClassName.Company, _context);
        }
        public async Task<ApiResponse> ProcessGet(Guid _MenuId, ClaimsPrincipal _User)
        {
            if (_AbsBusiness != null)
            {
                var response = await _AbsBusiness.GetDataAsync(_User);

                if (Convert.ToInt32(response.statusCode) == 200)
                {

                    var apiResponseUser = await _SecurityHelper.UserMenuPermissionAsync(_MenuId, _User);
                    if (apiResponseUser.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponseUser; }
                    var _UserMenuPermissionAsync = (GetUserPermissionViewModel)apiResponseUser.data;


                    var _Table = (IEnumerable<Company>)response.data;
                    var result = (from ViewTable in _Table
                                  select new CompanyViewModel
                                  {
                                      Id = ViewTable.Id,
                                      Name = ViewTable.Name,
                                      ShortName = ViewTable.ShortName,
                                      STN = ViewTable.STN,
                                      NTN = ViewTable.NTN,
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
            if (_AbsBusiness != null)
            {
                var response = await _AbsBusiness.GetDataByIdAsync(_Id, _User);
                if (Convert.ToInt32(response.statusCode) == 200)
                {
                    var _Table = (Company)response.data;
                    var _ViewModel = new CompanyViewByIdModel
                    {
                        Id = _Table.Id,
                        Name = _Table.Name,
                        ShortName = _Table.ShortName,
                        STN = _Table.STN,
                        NTN = _Table.NTN,
                        Type = _Table.Type,
                        Active = _Table.Active
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
            if (_AbsBusiness != null)
            {
                string _UserName = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserName.ToString())?.Value.ToString();
                var _request = (CompanyAddModel)request;


                string error = "";
                bool _ShortNameExists = _context.Companies.Any(rec => rec.ShortName.Trim().ToLower().Equals(_request.ShortName.Trim().ToLower()) && rec.Action != Enums.Operations.D.ToString());
                bool _NameExists = _context.Companies.Any(rec => rec.Name.Trim().ToLower().Equals(_request.Name.Trim().ToLower()) && rec.Action != Enums.Operations.D.ToString());

                if (_ShortNameExists)
                {
                    error = "Short Name,";
                }

                if (_NameExists)
                {
                    error = error + "Name";
                }

                if (_ShortNameExists || _NameExists)
                {
                    apiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                    apiResponse.message = error + " Already Exist";
                    return apiResponse;
                }
                var _Table = new Company
                {
                    Name = _request.Name,
                    ShortName = _request.ShortName,
                    NTN = _request.NTN,
                    STN = _request.STN,
                    Type = _request.Type,
                    Active = _request.Active,
                    UserNameInsert = _UserName,
                    InsertDate = DateTime.Now,
                    Action = Enums.Operations.A.ToString(),


                };
                apiResponse = await _AbsBusiness.AddAsync(_Table, _User);
                return apiResponse;
            }
            return null;
        }
        public async Task<ApiResponse> ProcessPut(object request, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            if (_AbsBusiness != null)
            {
                string _UserName = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserName.ToString())?.Value.ToString();
                var _request = (CompanyUpdateModel)request;

                string error = "";
                bool _ShortNameExists = _context.Companies.Any(rec => rec.ShortName.Trim().ToLower().Equals(_request.ShortName.Trim().ToLower()) && rec.Id != _request.Id && rec.Action != Enums.Operations.D.ToString());
                bool _NameExists = _context.Companies.Any(rec => rec.Name.Trim().ToLower().Equals(_request.Name.Trim().ToLower()) && rec.Id != _request.Id && rec.Action != Enums.Operations.D.ToString());

                if (_ShortNameExists)
                {
                    error = "Short Name,";
                }

                if (_NameExists)
                {
                    error = error + "Name";
                }

                if (_ShortNameExists || _NameExists)
                {
                    apiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                    apiResponse.message = error + " Already Exist";
                    return apiResponse;
                }

                var _result = _context.Companies.Where(a => a.Id == _request.Id && a.Action != Enums.Operations.D.ToString()).FirstOrDefault();
                if (_result == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found ";
                    return apiResponse;
                }


                _result.Name = _request.Name;
                _result.ShortName = _request.ShortName;
                _result.NTN = _request.NTN;
                _result.STN = _request.STN;
                _result.Type = _request.Type;
                _result.Active = _request.Active;
                _result.UserNameUpdate = _UserName;
                _result.Action = Enums.Operations.E.ToString();
                _result.UpdateDate = DateTime.Now;

                return await _AbsBusiness.UpdateAsync(_result, _User);
            }
            return null;

        }
        public async Task<ApiResponse> ProcessDelete(object request, ClaimsPrincipal _User)
        {

            if (_AbsBusiness != null)
            {
                var _request = (CompanyDeleteModel)request;

                return await _AbsBusiness.DeleteAsync(_request.Id, _User);
            }
            return null;
        }

    }
}