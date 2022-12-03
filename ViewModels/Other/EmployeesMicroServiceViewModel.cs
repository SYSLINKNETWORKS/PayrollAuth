using System;
using System.ComponentModel.DataAnnotations;
namespace TWP_API_Auth.ViewModels
{
    public class EmployeesMicroServiceViewModel
    {
        public Guid Id { get; set; }
        public int MachineId { get; set; }

        public string Name { get; set; }
        public string FatherName { get; set; }
        public string FullName { get; set; }

    }
    public class EmployeeByIdMicroServiceViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public bool Salesman { get; set; }
        public bool Director { get; set; }

    }

}