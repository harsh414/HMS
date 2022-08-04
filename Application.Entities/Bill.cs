using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class Bill
    {
        public int id { get; set; }
        public int p_id { get; set; }
        public float consultancy_fee { get; set; }
        public string room_type { get; set; }
        public int no_of_days { get; set; }
        public string dr_name { get; set; }
        public float op_charge { get; set; }
        public float room_charge { get; set; }
        public float test_charge { get; set; }
        public float med_charge { get; set; }
        public float final_bill  { get; set; }
    }
}
