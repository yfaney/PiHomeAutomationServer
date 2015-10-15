using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PiHomeAutomation.Models;

namespace PiHomeAutomation.Controllers
{
    public class HTSensorsController : Controller
    {
        private HTSensorDbContext db = new HTSensorDbContext();

        // GET: HTSensors
        public ActionResult Index()
        {
            return View(db.HTSensors.ToList());
        }

        public ActionResult Graph(string sensorName, int? dateAgo)
        {
            if(sensorName == null)
            {
                DateTime oneMonthAgo = DateTime.Now.AddDays(-30);
                return View(db.HTSensors.Where(m => m.CreatedOn >= oneMonthAgo).ToList());
            }
            else
            {
                ViewBag.SensorName = sensorName;
                if(dateAgo == null)
                {
                    DateTime oneMonthAgo = DateTime.Now.AddDays(-30);
                    return View(db.HTSensors.Where(m => m.SensorName == sensorName && m.CreatedOn >= oneMonthAgo).ToList());
                }
                else
                {
                    DateTime ago = DateTime.Now.AddDays(-(int)dateAgo);
                    return View(db.HTSensors.Where(m => m.SensorName == sensorName && m.CreatedOn >= ago).ToList());
                }
            }
        }

        // GET: HTSensors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HTSensor hTSensor = db.HTSensors.Find(id);
            if (hTSensor == null)
            {
                return HttpNotFound();
            }
            return View(hTSensor);
        }

        // GET: HTSensors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HTSensors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SensorName,Temperature,Humidity,Message")] HTSensor hTSensor)
        {
            if (ModelState.IsValid)
            {
                hTSensor.CreatedOn = DateTime.Now;
                db.HTSensors.Add(hTSensor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hTSensor);
        }

        // GET: HTSensors/Edit/5
        public ActionResult Edit(string sensor, DateTime createdOn)
        {
            if (sensor == null || createdOn == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HTSensor hTSensor = db.HTSensors.Find(sensor, createdOn);
            if (hTSensor == null)
            {
                return HttpNotFound();
            }
            return View(hTSensor);
        }

        // POST: HTSensors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SensorName,CreatedOn,Temperature,Humidity,Message")] HTSensor hTSensor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hTSensor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hTSensor);
        }

        // GET: HTSensors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HTSensor hTSensor = db.HTSensors.Find(id);
            if (hTSensor == null)
            {
                return HttpNotFound();
            }
            return View(hTSensor);
        }

        // POST: HTSensors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HTSensor hTSensor = db.HTSensors.Find(id);
            db.HTSensors.Remove(hTSensor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
