using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mvc_angela_trifunoska.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public ICollection<StudentSubject> Subjects { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First name")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last name")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter the  index")]
        public int Index { get; set; }
        
        public Student()
        {
            FullName = FirstName + " " + LastName;
        }
    }
}
