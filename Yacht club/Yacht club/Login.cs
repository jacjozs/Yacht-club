using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yacht_club
{
    class Login
    {
        public int id { get; set; }
        public string felhasznalonev { get; set; }
        public string jelszo { get; set; }
        public string email { get; set; }
        public bool admin { get; set; }
        public DateTime utolsoLogin { get; set; }
    }
}
