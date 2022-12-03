using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWP_API_Auth.Models
{
    [Table("UserItemCategory")]
    public class UserItemCategory
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public Guid ItemCategoryId { get; set; }


    }
}