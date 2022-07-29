using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class Patient
    {
        public int id { get; set; }
        public string p_name { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public int dept_id { get; set; }
        public int dr_id { get; set; }
        public bool Is_assigned { get; set; }
        public int assigned_to_dr_id { get; set; }
    }
}
