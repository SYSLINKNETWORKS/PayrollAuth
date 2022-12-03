using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Auth.ViewModels {
    public class ErrorLogModelBaseModel {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid Id { get; set; }
        [Required]
        public string ErrorId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }

    }

}