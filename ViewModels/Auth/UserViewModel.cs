using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWP_API_Auth.ViewModels
{
    public class UserBaseModel
    {

    }

    public class UserFoundationModel : UserBaseModel
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public String Employee { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool Active { get; set; }
        public int BackLog { get; set; }

        public bool Ck_OnlineAttandance { get; set; }
        public bool Ck_RequiredAttandance { get; set; }
    }

    public class UserViewModel : UserFoundationModel
    {
        [Required]
        public string Id { get; set; }

        [StringLength(250)]
        public string CompanyName { get; set; }
        public string RoleName { get; set; }
        public string ItemCategoryName { get; set; }

        [StringLength(250)]
        public string BranchName { get; set; }

        [Required]
        public bool NewPermission { get; set; }

        [Required]
        public bool UpdatePermission { get; set; }

        [Required]
        public bool DeletePermission { get; set; }

    }

    public class UserViewByIdModel : UserFoundationModel
    {
        [Required]
        public string Id { get; set; }

        [StringLength(250)]
        public string CompanyName { get; set; }

        [StringLength(250)]
        public string BranchName { get; set; }
        public Guid? EmployeeId { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public Guid BranchId { get; set; }

        [Required]
        public bool AllBranchCheck { get; set; }

        [Required]
        public DateTime PermissionFrom { get; set; }

        [Required]
        public DateTime PermissionTo { get; set; }
        public List<UserRolesViewModels> UserRolesViewModels { get; set; }
        public List<UserItemCategoryViewModels> UserItemCategoryViewModels { get; set; }
    }

    public class UserAddModel : UserFoundationModel
    {
        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Guid EmployeeId { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public Guid BranchId { get; set; }

        [Required]
        public bool AllBranchCheck { get; set; }

        [Required]
        public DateTime PermissionFrom { get; set; }

        [Required]
        public DateTime PermissionTo { get; set; }
        public List<UserRoles> userRolesList { get; set; }
        public List<UserItemCategoryViewModels> UserItemCategoryViewModels { get; set; }

    }
    public class UserUpdateModel : UserFoundationModel
    {

        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public string Id { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public Guid BranchId { get; set; }

        public Guid EmployeeId { get; set; }
        [Required]
        public bool AllBranchCheck { get; set; }

        [Required]
        public DateTime PermissionFrom { get; set; }

        [Required]
        public DateTime PermissionTo { get; set; }
        public List<UserRoles> userRolesList { get; set; }
        public List<UserItemCategoryViewModels> UserItemCategoryViewModels { get; set; }

    }
    public class UserDeleteModel : UserBaseModel
    {

        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public string Id { get; set; }
    }
    public class UserRoles
    {

        [Required]
        public string RoleName { get; set; }

    }
    public class UserRolesViewModels
    {

        [Required]
        public string RoleName { get; set; }
        public string UserId { get; set; }

    }

    public class UserItemCategoryViewModels
    {

        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }



    }

}