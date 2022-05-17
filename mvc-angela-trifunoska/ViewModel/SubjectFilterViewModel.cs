using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvc_angela_trifunoska.Models;

namespace mvc_angela_trifunoska.ViewModel
{
    public class SubjectFilterViewModel
    {

        public IList<Subject> Subjects { get; set; }
      
        public string subjectName { get; set; }
        public int semester { get; set; }
        public string program { get; set; }

        public SubjectFilterViewModel()
        {
        }
    }
}
