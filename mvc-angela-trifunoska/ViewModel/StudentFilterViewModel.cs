using System;
using System.Collections.Generic;
using mvc_angela_trifunoska.Models;

namespace mvc_angela_trifunoska.ViewModel
{
    public class StudentFilterViewModel
    {
        public IList<Student> Students { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public int index { get; set; }
        

        public StudentFilterViewModel()
        {
        }
    }
}
