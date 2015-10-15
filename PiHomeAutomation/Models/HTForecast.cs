using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PiHomeAutomation.Models
{
    public class HTForecast
    {
        [Key]
        [Column(Order = 0)]
        public string Zipcode { set; get; }
        [Key]
        [Column(Order = 1)]
        public DateTime ForecastDate { set; get; }
        public double Temperature { set; get; }
        public double Humidity { set; get; }
        public string Message { set; get; }
    }

    public class HTForecastEqualtyComparer : IEqualityComparer<HTForecast>
    {
        public bool Equals(HTForecast b1, HTForecast b2)
        {
            if (b2 == null & b1 == null)
                return true;
            else if (b1 == null | b2 == null)
                return false;
            else if (b1.Zipcode == b2.Zipcode
                    & b1.ForecastDate.Equals(b2.ForecastDate)
                    & b1.Temperature == b2.Temperature
                    & b1.Humidity == b2.Humidity
                    & b1.Message == b2.Message)
                return true;
            else
                return false;
        }

        public int GetHashCode(HTForecast bx)
        {
            return bx.Zipcode.GetHashCode();
        }
    }

    public class HTForecastDbContext : DbContext
    {
        public DbSet<HTForecast> HTForecasts { get; set; }
    }
}