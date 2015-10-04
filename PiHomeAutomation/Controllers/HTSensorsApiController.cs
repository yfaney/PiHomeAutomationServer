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

namespace PiHomeAutomation.Controllers
{
    public class HTSensorsApiController : ApiController
    {
        private HTSensorDbContext db = new HTSensorDbContext();

        // GET: api/HTSensorsApi
        public IQueryable<HTSensor> GetHTSensors()
        {
            return db.HTSensors;
        }
        [HttpGet]
        [Route(@"api/HTSensorsList/{SensorName}/{CreatedFrom:regex(^\d{4}-\d{2}-\d{2})}/{CreatedTo:regex(^\d{4}-\d{2}-\d{2})}")]
        public IQueryable<HTSensor> HTSensorsList(string SensorName, DateTime CreatedFrom, DateTime CreatedTo)
        {
            if(CreatedFrom == null || CreatedTo == null)
            {
                return db.HTSensors.Where(m => m.SensorName == SensorName);
            }
            else
            {
                DateTime from = CreatedFrom;
                DateTime to = CreatedTo;
                return db.HTSensors.Where(m => m.SensorName == SensorName
                                            && m.CreatedOn >= from
                                            && m.CreatedOn <= to);
            }
        }

        // GET: api/HTSensorsApi/5
        [ResponseType(typeof(HTSensor))]
        public IHttpActionResult GetHTSensor(string id)
        {
            HTSensor hTSensor = db.HTSensors.Find(id);
            if (hTSensor == null)
            {
                return NotFound();
            }

            return Ok(hTSensor);
        }

        // PUT: api/HTSensorsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHTSensor(string id, HTSensor hTSensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hTSensor.SensorName)
            {
                return BadRequest();
            }
            hTSensor.CreatedOn = DateTime.Now;
            db.Entry(hTSensor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HTSensorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/HTSensorsApi
        [ResponseType(typeof(HTSensor))]
        public IHttpActionResult PostHTSensor(HTSensor hTSensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HTSensors.Add(hTSensor);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HTSensorExists(hTSensor.SensorName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hTSensor.SensorName }, hTSensor);
        }

        // DELETE: api/HTSensorsApi/5
        [ResponseType(typeof(HTSensor))]
        public IHttpActionResult DeleteHTSensor(string id)
        {
            HTSensor hTSensor = db.HTSensors.Find(id);
            if (hTSensor == null)
            {
                return NotFound();
            }

            db.HTSensors.Remove(hTSensor);
            db.SaveChanges();

            return Ok(hTSensor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HTSensorExists(string id)
        {
            return db.HTSensors.Count(e => e.SensorName == id) > 0;
        }
    }
}