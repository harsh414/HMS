using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class Room
    {
        public int id { get; set; } 
        public string room_type { get; set; } 
        public string room_status { get; set; } 
        public int occupancy { get; set; } 
        public float price { get; set; } 
    }
}
