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
            
            Console.WriteLine(string.Format("共收到{0}筆紫外線監測資料", sts.Count));
            Console.WriteLine("==========================================");
            Console.WriteLine("（1）依縣市列表（2）依發布機關列表（3）全部列表（0）離開");
            Console.WriteLine("==========================================");
            Console.Write("請選擇列表方式：");
            var choose = Console.ReadLine();
            while (show_01(sts, choose))
            {
                Console.Clear();
                Console.WriteLine(string.Format("共收到{0}筆紫外線監測資料", sts.Count));
                Console.WriteLine("==========================================");
                Console.WriteLine("（1）依縣市列表（2）依發布機關列表（3）全部列表（0）離開");
                Console.WriteLine("==========================================");
                Console.Write("請選擇列表方式：");
                choose = Console.ReadLine();
            }
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
        public static bool show_01(List<station> stations , string choose)
        {
            Console.Clear();
            bool flag = true;
            bool flag_c = true;
            List<station> st = new List<station>();
            string choose_dn = "";
            switch (choose)
            {
                case "1":
                    Console.Write("輸入縣市名稱：");
                    var input_County = Console.ReadLine();
                    Console.WriteLine("==========================================");
                    var queryC = from i in stations
                                where i.County == input_County
                                select i;
                    Console.WriteLine("找到［縣市名稱］為{0}的資料有{1}筆", input_County, queryC.Count());
                    Console.WriteLine("==========================================");
                    search(queryC, st);
                    if (queryC.Count() == 0) flag_c = false;          
                    break;
                case "2":
                    Console.Write("選擇機關｛（1）環境保護署（2）中央氣象局｝：");   
                    var input_PublishAgency = Console.ReadLine();
                    Console.WriteLine("==========================================");
                    string input_PAstr="";
                    if (input_PublishAgency == "1") input_PAstr = "環境保護署";
                    else if (input_PublishAgency == "2") input_PAstr = "中央氣象局";
                    var queryP = from i in stations
                                where i.PublishAgency == input_PAstr
                                 select i;
                    Console.WriteLine("找到［發布機關］為{0}的資料有{1}筆", input_PAstr , queryP.Count());
                    Console.WriteLine("==========================================");
                    search(queryP, st);
                    break;
                case "3":
                    Console.WriteLine(string.Format("共收到{0}筆紫外線監測資料", stations.Count));
                    var queryA = from i in stations
                                 where i.County!=""
                                 select i;
                    search(queryA, st);
                    
                    break;
                default:
                    flag = false;
                    break;
            }
            if (flag & flag_c)
            {
                Console.Write("請輸入編號查看詳細資料：");
                choose_dn = Console.ReadLine();
                show_detail(st, choose_dn);
                Console.WriteLine("按任意鍵回主畫面");
                Console.ReadLine();
            }
            else if(!flag_c)
            {
                Console.WriteLine("兩秒後回主畫面");
                System.Threading.Thread.Sleep(2000);
            }
            st.Clear();
            return flag;
        }
        public static void search(IEnumerable<station> query,List<station> st)
        {
            int a = 1;
            string str = "";
            foreach (var q in query)
            {
                if (a < 10) str = "0" + a.ToString();
                else str = a.ToString();
                a++;
                st.Add(q);
                Console.WriteLine("（{0}）測站名稱 = {1}, 紫外線指數 = {2}", str, q.SiteName, q.UVI);
            }
        }
        public static void show_detail(List<station> st, string choose_dn)
        {      
            int a = Convert.ToInt16(choose_dn) - 1;
            if(a<st.Count)
            {
                Console.Clear();
                Console.WriteLine(" 測  站  名  稱 ： {0}",st[a].SiteName);
                Console.WriteLine("==========================================");
                Console.WriteLine(" 紫 外 線 指 數 ： {0}", st[a].UVI);
                Console.WriteLine("==========================================");
                Console.WriteLine(" 發  布  機  關 ： {0}", st[a].PublishAgency);
                Console.WriteLine("==========================================");
                Console.WriteLine(" 縣          市 ： {0}", st[a].County);
                Console.WriteLine("==========================================");
                Console.WriteLine(" 經          度 ： {0}", st[a].WGS84Lon);
                Console.WriteLine("==========================================");
                Console.WriteLine(" 緯          度 ： {0}", st[a].WGS84Lat);
                Console.WriteLine("==========================================");
                Console.WriteLine(" 發  布  時  間 ： {0}", st[a].PublishTime);
                Console.WriteLine("==========================================");
            }  
        }
    }
}
