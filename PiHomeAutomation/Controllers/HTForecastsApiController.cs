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
using System.Web.Script.Serialization;

namespace PiHomeAutomation.Controllers
{
    /// <summary>
    /// HT Forecast API
    /// </summary>
    public class HTForecastsApiController : ApiController
    {
        private HTForecastDbContext db = new HTForecastDbContext();

        // GET: api/HTForecasts
        public IQueryable<HTForecast> GetHTForecasts()
        {
            return db.HTForecasts;
        }

        ///<summary>
        ///Get the forecast data
        ///</summary>
        // GET: api/HTForecasts/5
        [ResponseType(typeof(HTForecast))]
        public IHttpActionResult GetHTForecast(string id)
        {
            HTForecast hTForecast = db.HTForecasts.Find(id);
            if (hTForecast == null)
            {
                return NotFound();
            }

            return Ok(hTForecast);
        }

        ///<summary>Put HT Forecast data</summary>
        // PUT: api/HTForecasts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHTForecast(string id, HTForecast hTForecast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hTForecast.Zipcode)
            {
                return BadRequest();
            }

            db.Entry(hTForecast).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HTForecastExists(id))
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

        ///<summary>Post the HTSensor data</summary>
        // POST: api/HTForecasts
        [ResponseType(typeof(HTForecast))]
        public IHttpActionResult PostHTForecast(HTForecast hTForecast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HTForecasts.Add(hTForecast);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HTForecastExists(hTForecast.Zipcode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hTForecast.Zipcode }, hTForecast);
        }

        ///<summary>
        ///Post the forecast data
        ///</summary>
        ///<param name="Zipcode">Zipcode of the area</param>
        ///<param name="ForecastJson">Forecast data as Json format</param>
        ///<example>"[{'epoch':EPOCH_TIME, 'temp':TEMPERATURE, 'humidity':HUMIDITY}]"</example>
        // POST: api/HTForecasts
        [Route(@"api/HTForecasts/{Zipcode}")]
        [ResponseType(typeof(HTForecast))]
        public IHttpActionResult PostHTForecast(string Zipcode, [FromBody]string ForecastJson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var result = serializer.Deserialize<HTForecastRaw[]>(ForecastJson);
            List<HTForecast> list = new List<HTForecast>();
            foreach (var item in result)
            {
                HTForecast forecast = new HTForecast() { Zipcode = Zipcode, ForecastDate = UnixTimeStampToDateTime(item.epoch), Temperature = item.temp, Humidity = item.humidity };
                HTForecastEqualtyComparer comparer = new HTForecastEqualtyComparer();
                if (db.HTForecasts.Any(fc => fc.Zipcode == Zipcode && fc.ForecastDate.Equals(forecast.ForecastDate)))
                {
                    db.Entry(forecast).State = EntityState.Modified;
                }
                else
                {
                    list.Add(forecast);
                }
            }
            if (list.Count() > 0)
            {
                db.HTForecasts.AddRange(list);
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            //return CreatedAtRoute("DefaultApi", new { id = Zipcode }, ForecastJson);
            return Ok(result);
        }

        ///<summary>Delete the corresponding forecast data</summary>
        // DELETE: api/HTForecasts/5
        [ResponseType(typeof(HTForecast))]
        public IHttpActionResult DeleteHTForecast(string id)
        {
            HTForecast hTForecast = db.HTForecasts.Find(id);
            if (hTForecast == null)
            {
                return NotFound();
            }

            db.HTForecasts.Remove(hTForecast);
            db.SaveChanges();

            return Ok(hTForecast);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HTForecastExists(string id)
        {
            return db.HTForecasts.Count(e => e.Zipcode == id) > 0;
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}