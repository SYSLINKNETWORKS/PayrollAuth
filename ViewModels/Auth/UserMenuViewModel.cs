using System;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Auth.ViewModels {
    public class UserMenuBaseModel {

    }

    public class UserMenuFoundationModel : UserMenuBaseModel {

        [Required]
        public Guid ModuleId { get; set; }

        public Guid SubCategoryId { get; set; }

        [Required]
        [StringLength (100)]
        public string Alias { get; set; }

        [Required]
        [StringLength (1)]
        public string Type { get; set; }

        public bool Active { get; set; }
        public bool View { get; set; }

    }
    public class UserMenuViewModel : UserMenuFoundationModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string ModuleName { get; set; }

        [Required]
        public string SubCategoryName { get; set; }

        [Required]
        [StringLength (100)]
        public string Name { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public bool NewPermission { get; set; }

        [Required]
        public bool UpdatePermission { get; set; }

        [Required]
        public bool DeletePermission { get; set; }

    }
    public class UserMenuViewByIdModel : UserMenuFoundationModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength (100)]
        public string Name { get; set; }

        [Required]
        public string Module_Name { get; set; }

        [Required]
        public string SubCategory_Name { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        [Required]
        public string CompanyName { get; set; }
    }
    public class UserMenuAddModel : UserMenuFoundationModel {
        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        [StringLength (100)]
        public string Name { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }
    public class UserMenuUpdateModel : UserMenuFoundationModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength (100)]
        public string Name { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }
    public class UserMenuDeleteModel : UserMenuBaseModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }
    }

}