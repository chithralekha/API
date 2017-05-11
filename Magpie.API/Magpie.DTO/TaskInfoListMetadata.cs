using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.DTO
{
    public class TaskInfoListMetadata
    {
        public int Count { get; set; }
        public int? FilterId { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
        public string SortBy { get; set; }   // SEE THIS FOR SORT ORDER ENUMS http://stackoverflow.com/questions/24720357/asp-net-webapi-enum-parameter-with-default-value
        public string SortOrder { get; set; }
    }
}
