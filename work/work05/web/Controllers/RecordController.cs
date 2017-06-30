using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class RecordController : Controller
    {
        public YON.Repository.RecordRepository RD_Db = new YON.Repository.RecordRepository();
        // GET: Record
        public ActionResult Index(string SiteName)
        {
            List<YON.Models.Record> model = new List<YON.Models.Record>();
            model = RD_Db.FindBySiteName(SiteName);
            return View(model);
        }
    }
}