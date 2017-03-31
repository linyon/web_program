using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YON.Model
{
    public class Station
    {
        public string ID { get; set; }
        public string LocationAddress { get; set; }
        public string ObservatoryName { get; set; }
        public string LocationByTWD67 { get; set; }
        public DateTime CreateTime { get; set; }


        //public string locationAddress;

        //public string LocationAddress
        //{
        //    get
        //    {
        //        return locationAddress;
        //    }
        //    set
        //    {
        //        if (value.Length < 10)
        //            locationAddress = value;
        //        else
        //            locationAddress = "";
        //    }
        //}


    }
}
