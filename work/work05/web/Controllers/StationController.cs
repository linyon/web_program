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
            if (!string.IsNullOrEmpty(search))
                stations = stations
                    .Where(x =>
                    x.SiteName.Contains(search) ||
                    x.PublishAgency.Contains(search))
                    .ToList();
            ViewBag.Search = search;
            //ViewBag.Stations = stations;

            return View(stations);
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
    }
}