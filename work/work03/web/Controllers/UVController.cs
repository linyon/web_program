using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class UVController : Controller
    {
        // GET: UV
        public ActionResult Index()
        {
            var st_Repository = new YON.Repository.StationRepository();
            var sts = st_Repository.FindAllSt();
            var message = string.Format("共收到{0}筆紫外線監測資料<br/>", sts.Count);
            message += string.Format("=============================<br/>");
            sts.ForEach(x =>
            {
                message += string.Format("站點名稱：{0}（UVI：{1}）<br/>", x.SiteName, x.UVI);
            });
            return Content(message);
        }
    }
}