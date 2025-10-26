using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    public class StatusDto
    {
        public string id { get; set; }
        public string status { get; set; }
        public int name { get; set; }
        public List<int> description { get; set; }

        public StatusDto() { }
    }
}
