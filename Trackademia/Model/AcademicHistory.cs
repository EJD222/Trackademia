using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackademia.Model
{
    public class AcademicHistory
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentNumber { get; set; }
        public string Level { get; set; }
        public string SchoolYear { get; set; }
        public string Semester { get; set; }
        public int Grade { get; set; }
        public int Program { get; set; }
        public string ProgramName { get; set; }
    }
}
