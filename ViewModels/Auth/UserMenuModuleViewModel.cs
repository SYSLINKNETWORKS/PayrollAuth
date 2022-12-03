using System;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Auth.ViewModels {
    public class UserMenuModuleBaseModel {

    }

    public class UserMenuModuleFoundationModel : UserMenuModuleBaseModel {

        [Required]
        [StringLength (100)]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        [StringLength (1)]
        public string Type { get; set; }

        public bool Active { get; set; }
        public bool View { get; set; }

        [Required]
        public bool NewPermission { get; set; }

        [Required]
        public bool UpdatePermission { get; set; }

        [Required]
        public bool DeletePermission { get; set; }

    }
    public class UserMenuModuleViewModel : UserMenuModuleFoundationModel {
        [Required]
        public Guid Id { get; set; }

        // [Required]
        // [StringLength (100)]
        // public new string Name { get; set; }

        [Required]
        public string CompanyName { get; set; }
    }
    public class UserMenuModuleViewByIdModel : UserMenuModuleFoundationModel {
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
    public class UserMenuModuleAddModel : UserMenuModuleFoundationModel {
        [Required]
        public Guid Menu_Id { get; set; }

        // [Required]
        // [StringLength (100)]
        //  public new string Name { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }
    public class UserMenuModuleUpdateModel : UserMenuModuleFoundationModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }
    public class UserMenuModuleDeleteModel : UserMenuModuleBaseModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }
    }

}