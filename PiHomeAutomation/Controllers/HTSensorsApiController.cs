using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PiHomeAutomation.Models;
using PiHomeAutomation.Helper;

namespace PiHomeAutomation.Controllers
{
    /// <summary>
    /// HTSensor data Web API
    /// </summary>
    public class HTSensorsApiController : ApiController
    {
        private RestApi api = new RestApi();

        // GET: api/HTSensorsApi
        /// <summary>
        /// Get Humidity and Temperature Sensor Data up to 30 days.
        /// </summary>
        /// <returns>
        /// All sensor data from 30 days ago to current
        /// </returns>
        public string GetHTSensors()
        {
            DateTime oneMonthAgo = DateTime.Now.AddDays(-30);
            return api.getHTSensors();
        }
        /// <summary>
        /// Get All Humidity and Temperature Sensor Data. It might take long time to get all data
        /// </summary>
        /// <param name="SensorName">Sensor Name.</param>
        [HttpGet]
        [Route(@"api/HTSensorsList/{SensorName}/All")]
        public string HTSensorsAll(string SensorName)
        {
            return api.getHTSensors(SensorName);
        }
        /// <summary>
        /// Get Humidity and Temperature Sensor Data for the given date range.
        /// </summary>
        /// <param name="SensorName">Sensor Name.</param>
        /// <param name="CreatedFrom">From.</param>
        /// <param name="CreatedTo">To.</param>
        [HttpGet]
        [Route(@"api/HTSensorsList/{SensorName}/{CreatedFrom:regex(^\d{4}-\d{2}-\d{2})}/{CreatedTo:regex(^\d{4}-\d{2}-\d{2})}")]
        public string HTSensorsList(string SensorName, DateTime CreatedFrom, DateTime CreatedTo)
        {
            if(CreatedFrom == null || CreatedTo == null)
            {
                return api.getHTSensors(SensorName);
            }
            else
            {
                DateTime from = CreatedFrom;
                DateTime to = CreatedTo;
                return api.getHTSensors(from, to, SensorName);
            }
        }
        /// <summary>
        /// Get the lastest sensor data
        /// </summary>
        /// <param name="SensorName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/HTSensorsList/{SensorName}/Last")]
        public string HTSensorsLast(string SensorName)
        {
            return api.getRecentSensorData(SensorName);
        }
        /////<summary>
        /////Get a single data corresponding ID
        /////</summary>
        //// GET: api/HTSensorsApi/5
        //[ResponseType(typeof(HTSensor))]
        //public IHttpActionResult GetHTSensor(string id)
        //{
        //    HTSensor hTSensor = db.HTSensors.Find(id);
        //    if (hTSensor == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(hTSensor);
        //}

        /////<summary>Put the HT sensor data</summary>
        /////<param name="hTSensor">The sensor's data(SensorName, Temp, Humi, Msg)</param>
        /////<param name="id">The sensor ID</param>
        /////<returns>200 Ok, others otherwise</returns>
        //// PUT: api/HTSensorsApi/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutHTSensor(string id, HTSensor hTSensor)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != hTSensor.SensorName)
        //    {
        //        return BadRequest();
        //    }
        //    hTSensor.CreatedOn = DateTime.Now;
        //    db.Entry(hTSensor).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!HTSensorExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        /////<summary>Post the HT sensor data(Sensor name, Temp, Humi, Msg)</summary>
        /////<returns>The sensor data posted</returns>
        //// POST: api/HTSensorsApi
        //[ResponseType(typeof(HTSensor))]
        //public IHttpActionResult PostHTSensor(HTSensor hTSensor)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.HTSensors.Add(hTSensor);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (HTSensorExists(hTSensor.SensorName))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = hTSensor.SensorName }, hTSensor);
        //}

        /////<summary>Deletes the corresponding HT Sensor data</summary>
        /////<param name="id">The sensor's ID</param>
        /////<returns>200 Ok, 404 Not found, others otherwise</returns>
        //// DELETE: api/HTSensorsApi/5
        //[ResponseType(typeof(HTSensor))]
        //public IHttpActionResult DeleteHTSensor(string id)
        //{
        //    HTSensor hTSensor = db.HTSensors.Find(id);
        //    if (hTSensor == null)
        //    {
        //        return NotFound();
        //    }

        //    db.HTSensors.Remove(hTSensor);
        //    db.SaveChanges();

        //    return Ok(hTSensor);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool HTSensorExists(string id)
        //{
        //    return db.HTSensors.Count(e => e.SensorName == id) > 0;
        //}
    }
}