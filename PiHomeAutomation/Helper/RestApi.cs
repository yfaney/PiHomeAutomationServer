using PiHomeAutomation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace PiHomeAutomation.Helper
{
    public class RestApi
    {
        public RestApi()
        {

        }
        private static string getResponseData(WebResponse response)
        {
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Clean up the streams and the response.
            reader.Close();
            response.Close();
            return responseFromServer;
        }
        private long to_epoch(DateTime tstamp)
        {
            return (long)tstamp.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
        /// <summary>
        /// Retrieves All HT Sensor data
        /// </summary>
        /// <returns></returns>
        public string getHTSensors()
        {
            WebRequest request = WebRequest.Create("http://localhost:27018/iot_new/ht_sensor?fields={'_id':0}&limit=1048576");
            WebResponse response = request.GetResponse();
            string result = getResponseData(response);
            return result;
        }
        public string getHTSensors(string sensor_name)
        {
            string url = string.Format("http://localhost:27018/iot_new/ht_sensor?query={{'sensor_name':'{0}'}&fields={{'_id':0}}&limit=1048576", sensor_name);
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            string result = getResponseData(response);
            return result;
        }
        public string getHTSensors(DateTime date_from, DateTime date_to, string sensor_name)
        {
            long epoch_from = to_epoch(date_from);
            long epoch_to = to_epoch(date_to);
            string query = string.Format("query={{'sensor_name':'{0}','tstamp':{{'$gte':{{'$date':{1}}}, '$lte':{{'$date':{2}}}}}}}", sensor_name, epoch_from, epoch_to);
            string url = string.Format("http://localhost:27018/iot_new/ht_sensor?{0}&fields={{'_id':0}}&limit=1048576", query);
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            string result = getResponseData(response);
            return result;
        }

        public string getHTSensors(DateTime date_from, string sensor_name)
        {
            long epoch_from = to_epoch(date_from);
            string query = string.Format("query={{'sensor_name':'{0}','tstamp':{{'$gte':{{'$date':{1}}}}}}}", sensor_name, epoch_from);
            string url = string.Format("http://localhost:27018/iot_new/ht_sensor?{0}&fields={{'_id':0}}&limit=1048576", query);
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            string result = getResponseData(response);
            return result;
        }

        public string getRecentSensorData(string sensor_name)
        {
            string query = string.Format("query={{'sensor_name':'{0}'}}", sensor_name);
            string url = string.Format("http://localhost:27018/iot_new/ht_sensor?{0}&fields={{'_id':0}}&sort={{'tstamp':-1}}&limit=1", query);
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            string result = getResponseData(response);
            return result;
        }

        public string getHTForecast(DateTime date_from, DateTime date_to, string zipcode)
        {
            long epoch_from = to_epoch(date_from);
            long epoch_to = to_epoch(date_to);
            string query = string.Format("query={{'zipcode':'{0}','tstamp':{{'$gte':{{'$date':{1}}}, '$lte':{{'$date':{2}}}}}}}", zipcode, epoch_from, epoch_to);
            string url = string.Format("http://localhost:27018/iot_new/ht_forecast?{0}&fields={{'_id':0}}&limit=65535", query);
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            string result = getResponseData(response);
            return result;
        }
        public string getHTForecast(DateTime date_from, string zipcode)
        {
            long epoch_from = to_epoch(date_from);
            string query = string.Format("query={{'zipcode':'{0}','tstamp':{{'$gte':{{'$date':{1}}}}}}}", zipcode, epoch_from);
            string url = string.Format("http://localhost:27018/iot_new/ht_forecast?{0}&fields={{'_id':0}}&limit=65535", query);
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            string result = getResponseData(response);
            return result;
        }

    }
}