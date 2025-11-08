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
        public int Qty { get; set; }
        public string Describetion { get; set; }
        public BarberStatus BarStatus { get; set; }
        public string pro { get; set; }
        public ConcurrentBag<string> NumConcurrentBag { get; set; }
        public string[] StringDes { get; set; }
        public List<StatusDao> Status { get; set; }

        public CardDAO()
        {
            ID = "5566.4";
            Name = "Empty";
            Describetion = "none";
            Qty = 123;
            BarStatus = BarberStatus.Cutting;
            pro = "Cutting";
            NumConcurrentBag = new ConcurrentBag<string>() { "1", "2", "3", "4", "5" };
            StringDes = new string[] { "9", "8", "7", "6" };
            Status = new List<StatusDao>() { new StatusDao(), new StatusDao() };
        }
    }
}
