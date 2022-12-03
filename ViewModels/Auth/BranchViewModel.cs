using System;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Auth.ViewModels
{
    public class BranchBaseModel
    {

    }
    public class BranchFoundationModel : BranchBaseModel
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string ShortName { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(250)]
        public string Phone { get; set; }

        [StringLength(250)]
        public string Mobile { get; set; }
        [StringLength(250)]
        public string Fax { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Web { get; set; }

        public bool HeadOffice { get; set; }

        [Required]
        [StringLength(1)]
        public string Type { get; set; }

        public bool Active { get; set; }
    }
    public class BranchViewModel : BranchFoundationModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        public String CompanyName { get; set; }

        public bool NewPermission { get; set; }

        public bool UpdatePermission { get; set; }

        public bool DeletePermission { get; set; }



    }
    public class BranchViewByIdModel : BranchFoundationModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        public String CompanyName { get; set; }

        public String LogoHeader { get; set; }
        public String LogoFooter { get; set; }

    }
    public class BranchAddModel : BranchFoundationModel
    {
        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
        public String LogoHeader { get; set; }
        public String LogoFooter { get; set; }

    }
    public class BranchUpdateModel : BranchFoundationModel
    {

        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        public String LogoHeader { get; set; }
        public String LogoFooter { get; set; }

    }
    public class BranchDeleteModel : BranchBaseModel
    {
        [Required]
        public Guid Menu_Id { get; set; }

        [Required]
        public Guid Id { get; set; }
    }
}