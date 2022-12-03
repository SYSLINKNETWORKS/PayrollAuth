using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWP_API_Auth.ViewModels {

    public class RoleBaseModel {

    }
    public class RoleFoundationModel : RoleBaseModel {

        [StringLength (256)]
        [Display (Name = "Role")]
        [Required (ErrorMessage = "Role Name is required")]
        public string Name { get; set; }
    }
    public class RoleViewModel : RoleFoundationModel {
        [Required]
        public string Id { get; set; }

        [Required]
        public bool NewPermission { get; set; }

        [Required]
        public bool UpdatePermission { get; set; }

        [Required]
        public bool DeletePermission { get; set; }
    }
    public class RoleViewByIdModel : RoleFoundationModel {
        [Required]
        public string Id { get; set; }
        public List<MenuPerView> menuPerViews { get; set; }

    }

    public class RoleAddModel : RoleFoundationModel {
        [Required]
        public Guid Menu_Id { get; set; }

    }
    public class RoleUpdateModel : RoleFoundationModel {

        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public string Id { get; set; }
    }

    public class RoleDeleteModel : RoleBaseModel {

        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public string Id { get; set; }
    }
    public class MenuPerView {
        [Required]
        public Guid Module_Id { get; set; }

        [Required]
        public string Module_Name { get; set; }

        [Required]
        public string Category_Name { get; set; }

        [Required]
        public string SubCategory_Name { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public string Menu_Name { get; set; }

        [Required]
        public string Menu_Alias { get; set; }

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

    }
}