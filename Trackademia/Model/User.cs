using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackademia.Model
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string StudentId { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }

        public int Program { get; set; }
        public string ProgramName { get; set; }

    }
}
