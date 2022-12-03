using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TWP_API_Auth.Models;

namespace TWP_API_Auth.Models
{
    [Table("UserLoginAudit")]
    public partial class UserLoginAudit
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        public string Key { get; set; }

        [Required]
        public string Header { get; set; }

        [Required]
        public string WanIp { get; set; }

        [Required]
        public bool Status { get; set; } = false;

        [Required]
        public DateTime LoginDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));
        public DateTime LogoutDate { get; set; }
        public Guid? EmployeeId { get; set; }
        public bool CkDirector { get; set; }
        public bool CkSalesman { get; set; }

        [Required]
        public string UserId { get; set; }
        // Navigation Property
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUsers { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
        // Navigation Property
        [ForeignKey("CompanyId")]
        public Company Companies { get; set; }

        [Required]
        public Guid BranchId { get; set; }
        // Navigation Property
        [ForeignKey("BranchId")]
        public Branch Branches { get; set; }

        public Guid? YearId { get; set; }
        // Navigation Property
        [ForeignKey("YearId")]
        public FinancialYear FinancialYears { get; set; }

    }
}