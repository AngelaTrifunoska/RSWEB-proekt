using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mvc_angela_trifunoska.Models
{
    public class Professor
    {

        [Key]
        public int Id { get; set; }

        public ICollection<Subject> Subject { get; set; }

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

        [Required(ErrorMessage = "Please enter the level of education")]
        [Display(Name = "Education")]
        [StringLength(100)]
        public string Education { get; set; }

        [Required(ErrorMessage = "Please enter academic rank")]
        public string Rank { get; set; }


        public Professor()
        {
            FullName = FirstName + " " + LastName;
        }
    }
}
