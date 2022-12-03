using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWP_API_Auth.Models
{
    [Table("FinancialYear")]
    public partial class FinancialYear
    {
        [Key]
        public Guid Id { get; set; }


        [Required]
        public DateTime StartDate { get; set; }
        [Required]

        public int StartYear { get; set; }

        [Required]

        public int StartMonth { get; set; }

        [Required]

        public int StartDay { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        
        [Required]
        public int EndYear { get; set; }

        
        [Required]
        public int EndMonth { get; set; }

        
        [Required]
        public int EndDay { get; set; }


        [Required]
        public bool Active { get; set; }

        [StringLength(1)]
        public string Type { get; set; }

        [Required]
        [StringLength(1)]
        public string Action { get; set; }

        public Guid CompanyId { get; set; }

        public string UserNameInsert { get; set; }

        public DateTime? InsertDate { get; set; }

        public string UserNameUpdate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string UserNameDelete { get; set; }

        public DateTime? DeleteDate { get; set; }

    }


}
