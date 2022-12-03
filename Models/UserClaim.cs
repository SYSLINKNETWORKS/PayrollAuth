using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWP_API_Auth.Models
{
    public class UserClaim
    {
        public string ClaimType { get; set; }
    }
}