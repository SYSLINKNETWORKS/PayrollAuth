using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWP_API_Auth.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 1)]
        public string Password { get; set; }

        [Required]
        public String WanIp { get; set; }

        [Required]
        public String Header { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

    }

    public class LoginTwoFactorModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public String WanIp { get; set; }

        [Required]
        public String Header { get; set; }
    }

    public class LoginNextModel
    {
        [Required]
        public Guid BranchId { get; set; }

        [Required]
        public Guid YearId { get; set; }

    }
    public class UserLoginInfoBaseModel
    {

        [Required]
        public string UserId { get; set; }
        public string UserName { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
        public String CompanyName { get; set; }

        [Required]
        public Guid BranchId { get; set; }

        public String BranchName { get; set; }
        public Guid YearId { get; set; }
        public String YearName { get; set; }

        public String YearStartDate { get; set; }
        public String YearEndDate { get; set; }

    }
}