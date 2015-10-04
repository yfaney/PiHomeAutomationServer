using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PiHomeAutomation.Models
{
    public class HTSensor
    {
        [Key][Column(Order = 0)]
        public string SensorName { set; get; }
        [Key][Column(Order = 1)]
        public DateTime CreatedOn { set; get; }
        public double Temperature { set; get; }
        public double Humidity { set; get; }
        public string Message { set; get; }
    }

    public class HTSensorDbContext : DbContext
    {
        public DbSet<HTSensor> HTSensors { get; set; }
    }
}