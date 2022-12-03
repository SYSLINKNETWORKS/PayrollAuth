using System;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Auth.ViewModels
{
    public class CompanyBaseModel
    {

    }
    public class CompanyFoundationModel : CompanyBaseModel
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string ShortName { get; set; }

        public string NTN { get; set; }

        public string STN { get; set; }


        [Required]
        [StringLength(1)]
        public string Type { get; set; }

        public bool Active { get; set; }

    }
    public class CompanyViewModel : CompanyFoundationModel
    {
        [Required]
        public Guid Id { get; set; }

        public bool NewPermission { get; set; }

        public bool UpdatePermission { get; set; }

        public bool DeletePermission { get; set; }

    }
    public class CompanyViewByIdModel : CompanyFoundationModel
    {
        [Required]
        public Guid Id { get; set; }
    }
    public class CompanyAddModel : CompanyFoundationModel
    {

        [Required]
        public Guid Menu_Id { get; set; }
    }
    public class CompanyUpdateModel : CompanyFoundationModel
    {

        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public Guid Id { get; set; }
    }

    public class CompanyDeleteModel : CompanyBaseModel
    {
        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public Guid Id { get; set; }
    }
}