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
            var xml = XElement.Load(@"D:\Work\Web\work_0317-1\28E06316-FE39-40E2-8C35-7BF070FD8697.xml");
            XNamespace gml = @"http://www.opengis.net/gml/3.2";
            XNamespace twed = @"http://twed.wra.gov.tw/twedml/opendata";
            var sts_node = xml.Descendants(twed + "RiverStageObservatoryProfile").ToList();
            for (var i = 0; i < sts_node.Count(); i++)
            {
                var st_node = sts_node[i];

            }
            foreach (var st_node in sts_node)
            {

            }
            sts_node.Where(x => !x.IsEmpty).ToList().ForEach(st_node =>
             {
                 var BasinIdentifier = st_node.Element(twed + "BasinIdentifier").Value.Trim();
                 var ObservatoryName = st_node.Element(twed + "ObservatoryName").Value.Trim();
                 var LocationAddress = st_node.Element(twed + "LocationAddress").Value.Trim();

                 var LocationByTWD67pos = st_node.Element(twed + "LocationByTWD67").Descendants(gml + "pos").FirstOrDefault().Value.Trim();
                 var LocationByTWD97pos = st_node.Element(twed + "LocationByTWD97").Descendants(gml + "pos").FirstOrDefault().Value.Trim();
                 station st_data = new station();
                 st_data.ID = BasinIdentifier;
                 st_data.LocationAddress = LocationAddress;
                 st_data.LocationByTWD67 = LocationByTWD67pos;
                 st_data.ObservatoryName = ObservatoryName;
                 st_data.CreateTime = DateTime.Now;
                 st.Add(st_data);
             });
            return st;
        }
        public static void show(List<station> stations)
        {
            Console.WriteLine(string.Format("共收到{0}筆資料", stations.Count));
            stations.ForEach(x =>
            {
                Console.WriteLine(string.Format("站點名稱={0}，地點={1}", x.ObservatoryName , x.LocationAddress));
            });
        }
    }
}
