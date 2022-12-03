using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWP_API_Auth.ViewModels {
    public class ClaimBaseModel { }

    public class ClaimFoundationModel : ClaimBaseModel {
        [Display (Name = "Claim Name")]
        [Required (ErrorMessage = "Claim Type is required")]
        public string ClaimType { get; set; }

    }
    public class ClaimViewModel : ClaimFoundationModel {
        [Required]
        public Guid Id { get; set; }
        public string Menu_Name { get; set; }
    }

    public class ClaimViewByIdModel : ClaimFoundationModel {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }
        public string Menu_Name { get; set; }

    }

    public class ClaimAddModel : ClaimFoundationModel {
        public Guid Menu_Id { get; set; }

    }
    public class ClaimUpdateModel : ClaimFoundationModel {

        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public Guid Id { get; set; }
    }
    public class ClaimDeleteModel : ClaimBaseModel {
        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public Guid Id { get; set; }
    }

}