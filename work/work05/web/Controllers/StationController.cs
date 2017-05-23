using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class StationController : Controller
    {
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
    }
}