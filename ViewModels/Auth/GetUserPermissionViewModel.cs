using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Auth.ViewModels {
    public class GetUserPermissionViewModel {
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
        public bool Check_Permission { get; set; }

        [Required]
        public bool Approve_Permission { get; set; }

                [Required]
        public Guid CompanyId { get; set; }
        [Required]
        public Guid BranchId { get; set; }
        [Required]
        public Guid FinancialYearId { get; set; }

    }
}