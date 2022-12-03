using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TWP_API_Auth.Helpers;

namespace TWP_API_Auth.Processor {
    public interface IProcessor<T> {
        Task<ApiResponse> ProcessGet (Guid _MenuId, ClaimsPrincipal _User);
        Task<ApiResponse> ProcessGetById (Guid _Id, Guid _MenuId, ClaimsPrincipal _User);
        Task<ApiResponse> ProcessPost (object request, ClaimsPrincipal _User);
        Task<ApiResponse> ProcessPut (object request,  ClaimsPrincipal _User);
        Task<ApiResponse> ProcessDelete (object request, ClaimsPrincipal _User);
    }
}