using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mvc_angela_trifunoska.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        public ICollection<StudentSubject> Students { get; set; }

        [Required(ErrorMessage = "Please enter the name of the subject")]
        [Display(Name = "Subject")]
        [StringLength(30)]
        public string SubjectName { get; set; }

        [Required(ErrorMessage = "Please enter the name of the program")]
        [Display(Name = "Program")]
        [StringLength(30)]
        public string Program { get; set; }


        [Required(ErrorMessage = "Please enter semester")]
        [Display(Name = "Semester")]
        [StringLength(30)]
        public string Semester { get; set; }

        [Display(Name = "Professor")]
        public int FirstProfessorId { get; set; }
        public Professor FirstProfessor { get; set; }


        [Display(Name = "Professor")]
        public int SecondProfessorId { get; set; }
        public Professor SecondProfessor { get; set; }
        
        public Subject()
        {

        }
    }
}
