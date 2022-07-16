using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50 , ErrorMessage = "Maximum Length of Name is 50")]
        [MinLength(10 , ErrorMessage = "Minimum Length of Name is 10")]
        public string Name { get; set; }
        [Range(22 ,60 , ErrorMessage ="Age Must be Between 22 and 60")]
        public int? Age { get; set; }
        //[RegularExpression(@"^[0-9]{1,10}-")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        [Range(4000,8000 , ErrorMessage ="Salary Must be between 4000 and 8000")]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        
    }
}
