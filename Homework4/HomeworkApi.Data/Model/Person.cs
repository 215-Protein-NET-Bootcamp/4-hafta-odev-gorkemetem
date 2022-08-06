using System;
using System.ComponentModel.DataAnnotations;

namespace HomeworkApi.Data
{
    public class Person 
    {
        [Key]
        public int AccountId { get; set; }
        public string StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
