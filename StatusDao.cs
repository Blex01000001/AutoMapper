using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    public class StatusDao
    {
        public int id { get; set; }
        public int status { get; set; }
        public string name { get; set; }
        public List<string> description { get; set; }
        public StatusDao()
        {
            id = 0;
            status = 0;
            name = "987";
            description = new List<string> { "111", "222", "333" };
        }


    }
}
