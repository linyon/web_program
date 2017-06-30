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
            int a = 0;
            foreach (var i in model)
            {
                if (a == model.Count() - 1)
                {
                    ViewBag.Last = i.PublishTime;
                }
                else a++;
            }
            ViewBag.Message = SiteName;
            ViewBag.count_UVI = new Func<List<YON.Models.Record>, int[]>(count_UVI);
            return View(model);
        }
        private int[] count_UVI(List<YON.Models.Record> uvi)
        {
            var count = new int[12];
            foreach (var i in uvi)
            {
                if (i.UVI >= 11) count[11]++;
                else count[i.UVI]++;
            }
            for (int i = 0; i < count.Count(); i++)
            {
                if (uvi.Count() != 0) count[i] = (count[i] / uvi.Count()) * 100;
            }
            return count;
        }
    }
}