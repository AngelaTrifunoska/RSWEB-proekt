using System;
using System.Collections.Generic;
using mvc_angela_trifunoska.Models;

namespace mvc_angela_trifunoska.ViewModel
{
    public class ProfessorFilterViewModel
    {
        public IList<Professor> Professors { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string education { get; set; }
        public string rank { get; set; }


        public ProfessorFilterViewModel()
        {
        }
    }
}
