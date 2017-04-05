using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YON.Models;
namespace YON
{
    class Program
    {
        static void Main(string[] args)
        {
            var import = new YON.Service.ImportXmlService();
            var db = new YON.Repository.StationRepository();
            var sts = import.findst(@"D:\Work\Web\web_program\work\work02\uv.xml");
            if(db.)
            sts.ToList().ForEach(x => { db.Create(x); });
            ShowStation(sts);
        }
        public static void ShowStation(List<Station> stations)
        {

            Console.WriteLine(string.Format("共收到{0}筆監測站的資料", stations.Count));
            stations.ForEach(x =>
            {
                Console.WriteLine(string.Format("站點名稱：{0},地址:{1}", x.SiteName, x.UVI));
                

            });
            Console.ReadKey();

        }
    }
}
