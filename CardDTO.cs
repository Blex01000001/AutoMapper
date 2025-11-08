using AutoMapper.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    public class CardDTO
    {
        public double ID { get; set; }
        //public string Name { get; set; }
        //public string Describetion { get; set; }
        //public string Qty { get; set; }
        //public int Num { get; set; }
        public BarberStatus BarStatus { get; set; }
        //public BarberStatus pro { get; set; }
        //public List<int> NumConcurrentBag { get; set; }
        //public int[] StringDes { get; set; }
        //public List<StatusDto> Status { get; set; }


        public CardDTO()
        {
            //Num = 666;
        }

    }
}
