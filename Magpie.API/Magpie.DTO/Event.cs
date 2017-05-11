using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.DTO
{
    public class Event
    {
        public int Id { get; set; }
        public User InstigatorUser { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
