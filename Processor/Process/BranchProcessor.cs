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
    public class BranchProcessor : IProcessor<BranchBaseModel>
    {
        private DataContext _context;
        private AbsBusiness _AbsBusiness;

        private SecurityHelper _SecurityHelper = new SecurityHelper();
        public BranchProcessor(App_Data.DataContext context)
        {
            _context = context;
            _AbsBusiness = Builder.MakeBusinessClass(Enums.ClassName.Branch, _context);
        }
        public async Task<ApiResponse> ProcessGet(Guid _MenuId, ClaimsPrincipal _User)
        {
            if (_AbsBusiness != null)
            {
                var response = await _AbsBusiness.GetDataAsync(_User);

                if (Convert.ToInt32(response.statusCode) == 200)
                {


                    var _Table = (IEnumerable<Branch>)response.data;

                    var apiResponseUser = await _SecurityHelper.UserMenuPermissionAsync(_MenuId, _User);
                    if (apiResponseUser.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponseUser; }
                    var _UserMenuPermissionAsync = (GetUserPermissionViewModel)apiResponseUser.data;



                    var result = (from ViewTable in _Table
                                  select new BranchViewModel
                                  {
                                      Id = ViewTable.Id,
                                      Name = ViewTable.Name,
                                      ShortName = ViewTable.ShortName,
                                      Address = ViewTable.Address,
                                      Phone = ViewTable.Phone,
                                      Mobile = ViewTable.Mobile,
                                      Email = ViewTable.Email,
                                      Web = ViewTable.Web,
                                      Type = ViewTable.Type,
                                      HeadOffice = ViewTable.HeadOffice,
                                      CompanyId = ViewTable.CompanyId,
                                      CompanyName = ViewTable.Companies.Name,
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
                    var _Table = (Branch)response.data;
                    var _ViewModel = new BranchViewByIdModel
                    {
                        Id = _Table.Id,
                        Name = _Table.Name,
                        ShortName = _Table.ShortName,
                        Address = _Table.Address,
                        Phone = _Table.Phone,
                        Mobile = _Table.Mobile,
                        Email = _Table.Email,
                        Web = _Table.Web,
                        Fax=_Table.Fax,
                        HeadOffice = _Table.HeadOffice,
                        Type = _Table.Type,
                        CompanyId = _Table.CompanyId,
                        CompanyName = _Table.Companies.Name,
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


                var _request = (BranchAddModel)request;

                string error = "";
                bool _ShortNameExists = _context.Branches.Any(rec => rec.ShortName.Trim().ToLower().Equals(_request.ShortName.Trim().ToLower()) && rec.Action != Enums.Operations.D.ToString());
                bool _NameExists = _context.Branches.Any(rec => rec.Name.Trim().ToLower().Equals(_request.Name.Trim().ToLower()) && rec.Action != Enums.Operations.D.ToString());

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

                var _Table = new Branch
                {
                    Name = _request.Name,
                    ShortName = _request.ShortName,
                    Address = _request.Address,
                    Phone = _request.Phone,
                    Mobile = _request.Mobile,
                    Fax = _request.Fax,
                    Email = _request.Email,
                    Web = _request.Web,
                    HeadOffice = _request.HeadOffice,
                    CompanyId = _request.CompanyId,
                    LogoHeader = _request.LogoHeader,
                    LogoFooter = _request.LogoFooter,
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

                var _request = (BranchUpdateModel)request;

                string error = "";
                bool _ShortNameExists = _context.Branches.Any(rec => rec.ShortName.Trim().ToLower().Equals(_request.ShortName.Trim().ToLower()) && rec.Id != _request.Id && rec.Action != Enums.Operations.D.ToString());
                bool _NameExists = _context.Branches.Any(rec => rec.Name.Trim().ToLower().Equals(_request.Name.Trim().ToLower()) && rec.Id != _request.Id && rec.Action != Enums.Operations.D.ToString());

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

                var _result = _context.Branches.Where(a => a.Id == _request.Id && a.Action != Enums.Operations.D.ToString()).FirstOrDefault();
                if (_result == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found ";
                    return apiResponse;
                }

                _result.Name = _request.Name;
                _result.ShortName = _request.ShortName;
                _result.Address = _request.Address;
                _result.Phone = _request.Phone;
                _result.Mobile = _request.Mobile;
                _result.Fax = _request.Fax;
                _result.Email = _request.Email;
                _result.Web = _request.Web;
                _result.HeadOffice = _request.HeadOffice;
                _result.CompanyId = _request.CompanyId;
                _result.LogoHeader = _request.LogoHeader;
                _result.LogoFooter = _request.LogoFooter;
                _result.Type = _request.Type;
                _result.Active = _request.Active;
                _result.Action = Enums.Operations.E.ToString();
                _result.UserNameUpdate = _UserName;
                _result.UpdateDate = DateTime.Now;

                return await _AbsBusiness.UpdateAsync(_result, _User);
            }
            return null;

        }
        public async Task<ApiResponse> ProcessDelete(object request, ClaimsPrincipal _User)
        {
            if (_AbsBusiness != null)
            {
                var _request = (BranchDeleteModel)request;
                return await _AbsBusiness.DeleteAsync(_request.Id, _User);
            }
            return null;
        }

    }
}