
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TWP_API_Auth.Helpers;

namespace TWP_API_Auth.Generic
{
    public interface IFacade<T>
    {
        Task<ApiResponse> GetDataAsync(ClaimsPrincipal _User);
        Task<ApiResponse> GetDataByIdAsync(Guid _Id,ClaimsPrincipal _User);
        Task<ApiResponse> AddAsync(T model,ClaimsPrincipal _User);
        Task<ApiResponse> UpdateAsync(T model, ClaimsPrincipal _User);
        Task<ApiResponse> DeleteAsync(Guid _Id, ClaimsPrincipal _User);
    }
}