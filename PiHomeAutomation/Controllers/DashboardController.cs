using PiHomeAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiHomeAutomation.Controllers
{
    public class DashboardController : Controller
    {
        private HTSensorDbContext htSensorDb = new HTSensorDbContext();
        private HTForecastDbContext htForecastDb = new HTForecastDbContext();

        public ActionResult Dashboard(string sensorName, int? dateAgo)
        {
            string zipcode = "66204";
            if (sensorName == null)
            {
                DateTime oneMonthAgo = DateTime.Now.AddDays(-14);
                ViewData["HTSensors"] = htSensorDb.HTSensors.Where(m => m.CreatedOn >= oneMonthAgo).ToList();
                ViewData["HTForecasts"] = htForecastDb.HTForecasts.Where(m => m.Zipcode == zipcode && m.ForecastDate >= oneMonthAgo).ToList();
                return View();
            }
            else
            {
                ViewBag.SensorName = sensorName;
                if (dateAgo == null)
                {
                    DateTime oneMonthAgo = DateTime.Now.AddDays(-14);
                    ViewData["HTSensors"] = htSensorDb.HTSensors.Where(m => m.SensorName == sensorName && m.CreatedOn >= oneMonthAgo).ToList();
                    ViewData["HTForecasts"] = htForecastDb.HTForecasts.Where(m => m.Zipcode == zipcode && m.ForecastDate >= oneMonthAgo).ToList();
                    return View();
                }
                else
                {
                    DateTime ago = DateTime.Now.AddDays(-(int)dateAgo);
                    ViewData["HTSensors"] = htSensorDb.HTSensors.Where(m => m.CreatedOn >= ago).ToList();
                    ViewData["HTForecasts"] = htForecastDb.HTForecasts.Where(m => m.Zipcode == zipcode && m.ForecastDate >= ago).ToList();
                    return View();
                }
            }
        }
    }
}