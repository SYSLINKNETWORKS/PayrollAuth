using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWP_API_Auth.Models
{

    public partial class CheckInOutMachineListModel
    {

        public List<CheckInOutMachineModel> CheckInOutMachineModels { get; set; }
    }
    public class CheckInOutMachineModel 
    {
        [Required]
        public int UserId { get; set; }
        public DateTime CheckTime { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}