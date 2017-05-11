using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.DTO
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        public User LastModifiedByUser { get; set; }
        public string Text { get; set; }
    }
}
