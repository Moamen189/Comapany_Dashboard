using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class DepartmentViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Minimum Length of Name is 3 Characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }

        public DateTime DateOfCreation { get; set; }

        public virtual ICollection<Employee> Departments { get; set; } = new HashSet<Employee>();
    }
}
