using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace work_0317_1
{
    class Program
    {
        static void Main(string[] args)
        {

           var sts =  findst();
            show(sts);
            Console.ReadLine();

        }
        public static List<station> findst()
        {
            List<station> st = new List<station>();

            var xml = XElement.Load(@"D:\Work\Web\web_program\work\work01\work_0317-1\uv.xml");

            var sts_node = xml.Descendants("Data").ToList();
            for (var i = 0; i < sts_node.Count(); i++)
            {
                var st_node = sts_node[i];

            }
            foreach (var st_node in sts_node)
            {

            }
            sts_node.Where(x => !x.IsEmpty).ToList().ForEach(st_node =>
             {
                 var SiteName = st_node.Element("SiteName").Value.Trim();
                 var UVI = st_node.Element("UVI").Value.Trim();
                 var PublishAgency = st_node.Element("PublishAgency").Value.Trim();
                 var County = st_node.Element("County").Value.Trim();
                 var WGS84Lon = st_node.Element("WGS84Lon").Value.Trim();
                 var WGS84Lat = st_node.Element("WGS84Lat").Value.Trim();
                 var PublishTime = st_node.Element("PublishTime").Value.Trim();
                 station st_data = new station();
                 st_data.SiteName = SiteName;
                 st_data.UVI = UVI;
                 st_data.PublishAgency = PublishAgency;
                 st_data.County = County;
                 st_data.WGS84Lon = WGS84Lon;
                 st_data.WGS84Lat = WGS84Lat;
                 st_data.PublishTime = PublishTime;
                 st.Add(st_data);
             });
            return st;
        }
        public static void show(List<station> stations)
        {
            Console.WriteLine(string.Format("共收到{0}筆紫外線監測資料", stations.Count));
            Console.WriteLine("請選擇列表方式：（1）依縣市列表（2）依發布機關列表");
            var choose = Console.ReadLine();
            switch (choose)
            {
                case "1":
                    Console.WriteLine("輸入縣市名稱：");
                    var input_County = Console.ReadLine();
                    var queryC = from i in stations
                                where i.County == input_County
                                select i;
                    foreach (var q in queryC)
                    {
                        Console.WriteLine("測站名稱 = \"{0}\", 紫外線指數 = {1}", q.SiteName, q.UVI);
                    }
                    break;
                case "2":
                    Console.WriteLine("選擇機關：（1）環境保護署（2）中央氣象局");
                    var input_PublishAgency = Console.ReadLine();
                    string input_PAstr="";
                    if (input_PublishAgency == "1") input_PAstr = "環境保護署";
                    else if (input_PublishAgency == "2") input_PAstr = "中央氣象局";
                    var queryP = from i in stations
                                where i.PublishAgency == input_PAstr
                                 select i;
                    foreach (var q in queryP)
                    {
                        Console.WriteLine("測站名稱 = \"{0}\", 紫外線指數 = {1}", q.SiteName, q.UVI);
                    }
                    break;
                default:
                    break;
            }
            //stations.ForEach(x =>
            //{
            //    Console.WriteLine(string.Format("站點名稱={0}，紫外線指數={1}", x.SiteName, x.UVI));
            //});
        }
    }
}
