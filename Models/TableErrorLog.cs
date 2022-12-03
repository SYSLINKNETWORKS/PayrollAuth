using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWP_API_Auth.Models
{
    [Table("TableErrorLog")]
    public class TableErrorLog
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        [Required]
        public string ErrorId { get; set; }
        [Required]
        public string ErrorDescription { get; set; }
        [Required]
        public string ErrorType { get; set; }
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser applicationUser { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}