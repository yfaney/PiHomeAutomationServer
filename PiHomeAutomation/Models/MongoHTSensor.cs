using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PiHomeAutomation.Models
{
    /// <summary>
    /// Mongo object for HTSensor
    /// </summary>
    public class MongoHTSensor
    {
        public ObjectId _id { set; get; }
        public string sensor_name { set; get; }
        public DateTime tstamp { set; get; }
        public double temperature { set; get; }
        public double humidity { set; get; }
        public string message { set; get; }
    }

}