using AutoMapper.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    public class CardDAO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string qty { get; set; }
        public string Describetion { get; set; }
        //public int BarStatus { get; set; }
        //public string pro { get; set; }
        //public ConcurrentBag<string> NumConcurrentBag { get; set; }
        //public string[] StringDes { get; set; }
        //public List<StatusDao> Status { get; set; }

        public CardDAO()
        {
            ID = "5566";
            Name = "John";
            Describetion = "Describetion Describetion Describetion";
            qty = "250";
            //BarStatus = 0;
            //pro = "Cutting";
            //NumConcurrentBag = new ConcurrentBag<string>() { "1", "2", "3", "4", "5" };
            //StringDes = new string[] { "9", "8", "7", "6" };
            //Status = new List<StatusDao>() { new StatusDao(), new StatusDao() };
        }
    }
}
