using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Model
{
    public class StudyAreaView
    {
        public string Name { get; set; }

        public StudyAreaView() { }

        public StudyAreaView(StudyArea studyArea)
        {
            Name = studyArea.Name;
        }
    }
}
