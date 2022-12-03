using System;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Auth.ViewModels {
    public class UserMenuSubCategoryBaseModel {

    }

    public class UserMenuSubCategoryFoundationModel : UserMenuSubCategoryBaseModel {

        [Required]
        [StringLength (100)]
        public string Name { get; set; }

        [Required]
        public Guid CategoryId {get;set;}
        [Required]
        [StringLength (1)]
        public string Type { get; set; }

        public bool Active { get; set; }
        public bool View { get; set; }

    }
    public class UserMenuSubCategoryViewModel : UserMenuSubCategoryFoundationModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public bool NewPermission { get; set; }

        [Required]
        public bool UpdatePermission { get; set; }

        [Required]
        public bool DeletePermission { get; set; }
        
        public string CategoryName {get;set;}

        [Required]
        public string CompanyName { get; set; }
    }
    public class UserMenuSubCategoryViewByIdModel : UserMenuSubCategoryFoundationModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Icon { get; set; }

        public string CategoryName {get;set;}

        [Required]
        public Guid CompanyId { get; set; }

        [Required]
        public string CompanyName { get; set; }
    }
    public class UserMenuSubCategoryAddModel : UserMenuSubCategoryFoundationModel {
        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }
    public class UserMenuSubCategoryUpdateModel : UserMenuSubCategoryFoundationModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }
    public class UserMenuSubCategoryDeleteModel : UserMenuCategoryBaseModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }
    }

}