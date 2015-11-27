using PiHomeAutomation.Helper;
using PiHomeAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiHomeAutomation.Controllers
{
    /// <summary>
    /// Dashboard controller : /Dashboard
    /// </summary>
    public class DashboardController : Controller
    {

        private RestApi api = new RestApi();
        /// <summary>
        /// Gives dashboard page
        /// </summary>
        /// <param name="sensorName">Sensor name (e.g. LivingRoom01)</param>
        /// <param name="dateAgo">Number of dates ago to show</param>
        /// <returns></returns>
        public ActionResult Dashboard(string sensorName, int? dateAgo)
        {
            string zipcode = "66204";
            if (sensorName == null)
            {
                DateTime oneMonthAgo = DateTime.Now.AddDays(-1);
                ViewData["HTSensors"] = api.getHTSensors(oneMonthAgo, "LivingRoom01");
                ViewData["HTForecasts"] = api.getHTForecast(oneMonthAgo, zipcode);
                return View();
            }
            else
            {
                ViewBag.SensorName = sensorName;
                if (dateAgo == null)
                {
                    DateTime oneMonthAgo = DateTime.Now.AddDays(-1);
                    ViewData["HTSensors"] = api.getHTSensors(oneMonthAgo, sensorName);
                    ViewData["HTForecasts"] = api.getHTForecast(oneMonthAgo, zipcode);
                    return View();
                }
                else
                {
                    DateTime ago = DateTime.Now.AddDays(-(int)dateAgo);
                    ViewData["HTSensors"] = api.getHTSensors(ago, sensorName);
                    ViewData["HTForecasts"] = api.getHTForecast(ago, zipcode);
                    return View();
                }
            }
        }
    }
}