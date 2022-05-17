using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvc_angela_trifunoska.Models;

namespace mvc_angela_trifunoska.ViewModel
{
    public class SubjectStudentEditViewModel
    {

        public int Id { get; set; }
        public Student Student { get; set; }
        public IEnumerable<int> SelectedSubjects { get; set; }
        public IEnumerable<SelectListItem> SubjectList { get; set; }

        public SubjectStudentEditViewModel()
        {
        }
    }
}
