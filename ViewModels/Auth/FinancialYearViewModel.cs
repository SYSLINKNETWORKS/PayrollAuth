using System;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Auth.ViewModels
{
    public class FinancialYearBaseModel
    {
         [Required]
        public DateTime StartDate { get; set; }
        // [Required]

        // public int StartYear { get; set; }

        // [Required]

        // public int StartMonth { get; set; }

        // [Required]

        // public int StartDay { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        
        // [Required]
        // public DateTime EndYear { get; set; }

        
        // [Required]
        // public DateTime EndMonth { get; set; }

        
        // [Required]
        // public DateTime EndDay { get; set; }
        public bool Active { get; set; }
    }

    public class FinancialYearViewModel : FinancialYearBaseModel
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
         public bool NewPermission { get; set; }

        [Required]
        public bool UpdatePermission { get; set; }

        [Required]
        public bool DeletePermission { get; set; }
    }
    public class FinancialYearViewByIdModel : FinancialYearBaseModel
    {
        [Required]
        public Guid Id { get; set; }
        public string Type { get; set; }
    }
    public class FinancialYearAddModel : FinancialYearBaseModel
    {
        [Required]
        public Guid Menu_Id { get; set; }
    }
    public class FinancialYearUpdateModel : FinancialYearBaseModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid Menu_Id { get; set; }
    }
    public class FinancialYearDeleteModel : FinancialYearBaseModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid Menu_Id { get; set; }

    }
}