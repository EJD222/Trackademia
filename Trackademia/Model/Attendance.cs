using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackademia.Model
{
    public class Attendance
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentNumber { get; set; }
    }
}
