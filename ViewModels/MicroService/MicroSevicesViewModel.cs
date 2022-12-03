using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Auth.ViewModels
{
    public class UserPermissionViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }


        [Required]
        public Guid MenuId { get; set; }


        [Required]
        public string MenuName { get; set; }
        [Required]
        public string MenuAlias { get; set; }

        [Required]
        public bool View_Permission { get; set; }

        [Required]
        public bool Insert_Permission { get; set; }

        [Required]
        public bool Update_Permission { get; set; }

        [Required]
        public bool Delete_Permission { get; set; }
        [Required]
        public bool Print_Permission { get; set; }

        [Required]
        public bool Check_Permission { get; set; }

        [Required]
        public bool Approve_Permission { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        [Required]
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        [Required]
        public Guid FinancialYearId { get; set; }
        public string FinancialYearName { get; set; }
        public DateTime YearStartDate { get; set; }
        public DateTime YearEndDate { get; set; }
        public DateTime PermissionDateFrom { get; set; }
        public DateTime PermissionDateTo { get; set; }
        public Guid? EmployeeId { get; set; }
        public bool ckSalesman { get; set; }
        public bool ckDirector { get; set; }
    }

    public class BranchInfoViewModel
    {
        public Guid CompanyId { get; set; }
        public String CompanyName { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }

        public string ShortName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Web { get; set; }
        public string Fax { get; set; }
        public string NTN { get; set; }
        public string STN { get; set; }
        public string ImageUrlHeader { get; set; }
        public string ImageUrlFooter { get; set; }

        public bool HeadOffice { get; set; }

        public string Type { get; set; }

        public bool Active { get; set; }


    }

    public class UserLoginInfoViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserMobile { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }

        public Guid BranchId { get; set; }
        public string BranchName { get; set; }

        public Guid FinancialYearId { get; set; }
        public string FinancialYearName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool ckSalesman { get; set; }
        public bool ckDirector { get; set; }
        public Guid? EmployeeId { get; set; }
        public List<string> ItemCategoryIdList { get; set; }

    }
    public class MSFinancialYearViewModel
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class MSItemCategoryViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }


    }

    public class MenuInfoViewModel
    {

        public Guid MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuAlias { get; set; }
        public Guid ModuleId { get; set; }
        public string ModuleName { get; set; }
        public Guid SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public Guid SubCategoryMasterId { get; set; }
        public string SubCategoryMasterName { get; set; }


    }

}