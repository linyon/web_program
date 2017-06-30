using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class StationController : Controller
    {

        public YON.Repository.StationRepository ST_Db = new YON.Repository.StationRepository();
        // GET: Station
        public ActionResult Index(string search = "")
        {

            var stationRepository = new YON.Repository.StationRepository();

            var stations = stationRepository.FindAllSt();
            //foreach (var i in stations)
            //{
            //    var Lat = i.WGS84Lat;
            //    string[] lat_tmp = Lat.Split(',');
            //    float lat = float.Parse(lat_tmp[0]) + (float.Parse(lat_tmp[1]) / 60) + (float.Parse(lat_tmp[0]) / 3600);
            //    var Lon = i.WGS84Lon;
            //    string[] lon_tmp = Lon.Split(',');
            //    float lon = float.Parse(lon_tmp[0]) + (float.Parse(lon_tmp[1]) / 60) + (float.Parse(lon_tmp[0]) / 3600);
            //    i.WGS84Lat = lat.ToString();
            //    i.WGS84Lon = lon.ToString();
            //}
            if (!string.IsNullOrEmpty(search))
                stations = stations
                    .Where(x =>
                    x.SiteName.Contains(search) ||
                    x.PublishAgency.Contains(search))
                    .ToList();
            ViewBag.Search = search;
            ViewBag.Stations = stations;
            ViewBag.check_UVIclass = new Func<int, string>(check_UVIclass);
            return View(stations);
        }
        [HttpPost]
        public JsonResult GetAddress()
        {
            //test data
            var stationRepository = new YON.Repository.StationRepository();
            var stations = stationRepository.FindAllSt();
            var model = new List<YON.Models.Station>();
            foreach (var i in stations)
            {
                var Lat = i.WGS84Lat;
                string[] lat_tmp = Lat.Split(',');
                float lat = float.Parse(lat_tmp[0]) + (float.Parse(lat_tmp[1]) / 60) + (float.Parse(lat_tmp[0]) / 3600);
                var Lon = i.WGS84Lon;
                string[] lon_tmp = Lon.Split(',');
                float lon = float.Parse(lon_tmp[0]) + (float.Parse(lon_tmp[1]) / 60) + (float.Parse(lon_tmp[0]) / 3600);
                i.WGS84Lat = lat.ToString();
                i.WGS84Lon = lon.ToString();
                
            }
            return Json(new { AddressResult = stations });

        }
        [HttpGet]
        public ActionResult Update(string SiteName)
        {
            YON.Models.Station model;
            if (string.IsNullOrEmpty(SiteName))
                model = new YON.Models.Station();
            else
                model = ST_Db.FindBySiteName(SiteName);


            return View(model);
        }
        [HttpPost]
        public ActionResult Update(YON.Models.Station station)
        {
            ST_Db.Update(station);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(YON.Models.Station station)
        {
            return View();
        }
        private string check_UVIclass(int uvi)
        {
            string str = "";
            if (uvi < 3)
            {
                str = "w3-green";
            }
            else if (uvi < 6)
            {
                str = "w3-yellow";
            }
            else if (uvi < 8)
            {
                str = "w3-orange";
            }
            else if (uvi < 11)
            {
                str = "w3-red";
            }
            else
            {
                str = "w3-purple";
            }
            return str;
        }
    }
}