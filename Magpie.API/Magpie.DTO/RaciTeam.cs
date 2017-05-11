using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.DTO
{
    public class RaciTeam
    {
        //public int? Id { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        public User ResponsibleUser { get; set; }
        public User AccountableUser { get; set; }
        public IEnumerable<User> ConsultedUsers { get; set; }
        public IEnumerable<User> InformedUsers { get; set; }
    }
}
