using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiHomeAutomation.Models
{
    public class HTForecastRaw
    {
        public long epoch { set; get; }
        public double temp { set; get; }
        public double humidity { set; get; }
        public string message { set; get; }
    }
}