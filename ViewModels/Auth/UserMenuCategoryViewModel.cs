using System;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Auth.ViewModels {
    public class UserMenuCategoryBaseModel {

    }

    public class UserMenuCategoryFoundationModel : UserMenuCategoryBaseModel {

        [Required]
        [StringLength (100)]
        public string Name { get; set; }

        [Required]
        [StringLength (1)]
        public string Type { get; set; }

        public bool Active { get; set; }
        public bool View { get; set; }

    }
    public class UserMenuCategoryViewModel : UserMenuCategoryFoundationModel {
        [Required]
        public Guid Id { get; set; }

        
         [Required]
        public bool NewPermission { get; set; }

        [Required]
        public bool UpdatePermission { get; set; }

        [Required]
        public bool DeletePermission { get; set; }

        [Required]
        public string CompanyName { get; set; }
    }
    public class UserMenuCategoryViewByIdModel : UserMenuCategoryFoundationModel {
        [Required]
        public Guid Id { get; set; }

        // [Required]
        // [StringLength (100)]
        // public new string Name { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        [Required]
        public string CompanyName { get; set; }
    }
    public class UserMenuCategoryAddModel : UserMenuCategoryFoundationModel {
        [Required]
        public Guid Menu_Id { get; set; }

        // [Required]
        // [StringLength (100)]
        //  public new string Name { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }
    public class UserMenuCategoryUpdateModel : UserMenuCategoryFoundationModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }
    public class UserMenuCategoryDeleteModel : UserMenuCategoryBaseModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }
    }

}