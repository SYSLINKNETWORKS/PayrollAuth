using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Auth.ViewModels {
    public class UserRolePermissionBaseModel {

    }
    public class UserRolePermissionViewModel : UserRolePermissionBaseModel {
        [Required]
        public String Roles_Id { get; set; }

        [Required]
        public string Roles_Name { get; set; }

    }
    public class UserRolePermissionViewByIdModel : UserRolePermissionBaseModel {

        [Required]
        public String Roles_Id { get; set; }

        [Required]
        public string Roles_Name { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public string Menu_Name { get; set; }

        [Required]
        public bool View_Permission { get; set; }

        [Required]
        public bool Insert_Permission { get; set; }

        [Required]
        public bool Update_Permission { get; set; }

        [Required]
        public bool Delete_Permission { get; set; }
    }
    public class UserRolePermissionAddModel : UserRolePermissionBaseModel {
        public List<UserRolePermissionListModel> UserRolePermissions { get; set; }
    }
    public class UserRolePermissionUpdateModel : UserRolePermissionBaseModel {
        public List<UserRolePermissionListModel> UserRolePermissions { get; set; }
    }
    public class UserRolePermissionDeleteModel : UserRolePermissionBaseModel {
        [Required]
        public String Roles_Id { get; set; }

    }
    public class UserRolePermissionListModel {
        [Required]
        public String Roles_Id { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }

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
        public bool Approved_Permission { get; set; }

    }
}