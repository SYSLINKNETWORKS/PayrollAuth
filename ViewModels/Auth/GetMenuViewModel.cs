using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Auth.ViewModels {
    public class GetMenuBaseModel {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        public string FiscalYear { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Version { get; set; }

        [Required]
        public List<GetMenuUserWise> GetMenuUserWises { get; set; }

    }
    public class GetMenuViewModel : GetMenuBaseModel {
        // [Required]
        // public Guid Id { get; set; }
    }
    public class GetMenuViewByIdModel : GetMenuBaseModel {
        [Required]
        public Guid Id { get; set; }
    }
    public class GetMenuAddModel : GetMenuBaseModel {
        [Required]
        public Guid Menu_Id { get; set; }

    }
    public class GetMenuUpdateModel : GetMenuBaseModel {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid Menu_Id { get; set; }
    }
    public class GetMenuUserWise {
        [Required]
        public Guid MenuId { get; set; }

        [Required]
        public string MenuName { get; set; }

        [Required]
        public string MenuAlias { get; set; }
        [Required]
        public string MenuUrl { get; set; }

        [Required]
        public bool MenuView { get; set; }

        [Required]
        public Guid MenuCategoryId { get; set; }

        [Required]
        public string MenuCategoryName { get; set; }


        [Required]
        public Guid MenuSubCategoryId { get; set; }

        [Required]
        public string MenuSubCategoryName { get; set; }

        [Required]
        public string MenuSubCategoryIcon { get; set; }
        [Required]
        public Guid ModuleId { get; set; }

        [Required]
        public string ModuleName { get; set; }

        [Required]
        public string ModuleIcon { get; set; }
    }

        public class GetMenuReportUserWise {
        [Required]
        public Guid MenuId { get; set; }

        [Required]
        public string MenuName { get; set; }

        [Required]
        public string MenuAlias { get; set; }
    }
}