using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YON.Models;
namespace YON.Show
{
    public class Show
    {
        public bool ShowSt(List<Station> stations, string choose)
        {
            Console.Clear();
            bool flag = true;
            bool flag_query = true;
            List<Station> st = new List<Station>();
            string choose_dn = "";
            switch (choose)
            {
                case "1":
                    Console.Write("輸入測站名稱：");
                    var input_County = Console.ReadLine();
                    Console.WriteLine("==========================================");
                    var queryC = from i in stations
                                 where i.SiteName.Contains(input_County)
                                 select i;
                    Console.WriteLine("找到［測站名稱］為{0}的相關資料有{1}筆", input_County, queryC.Count());
                    Console.WriteLine("==========================================");
                    show_list(queryC, st);
                    if (queryC.Count() == 0) flag_query = false;
                    break;
                case "2":
                    Console.Write("選擇機關｛（1）環境保護署（2）中央氣象局｝：");
                    var input_PublishAgency = Console.ReadLine();
                    Console.WriteLine("==========================================");
                    string input_PAstr = "";
                    if (input_PublishAgency == "1") input_PAstr = "環境保護署";
                    else if (input_PublishAgency == "2") input_PAstr = "中央氣象局";
                    var queryP = from i in stations
                                 where i.PublishAgency == input_PAstr
                                 select i;
                    if (queryP.Count() != 0)
                    {
                        Console.WriteLine("找到［發布機關］為{0}的資料有{1}筆", input_PAstr, queryP.Count());
                        Console.WriteLine("==========================================");
                        show_list(queryP, st);
                    }
                    else flag_query = false;
                    break;
                case "3":
                    Console.WriteLine(string.Format("共收到{0}筆紫外線監測資料", stations.Count));
                    var queryA = from i in stations
                                 where i.County != ""
                                 select i;
                    show_list(queryA, st);
                    break;
                case "4":

                    break;
                default:
                    flag = false;
                    break;
            }
            if (flag & flag_query)
            {
                Console.Write("請輸入編號查看詳細資料：");
                choose_dn = Console.ReadLine();
                show_detail(st, choose_dn);
                Console.WriteLine("按任意鍵回主畫面");
                Console.ReadLine();
            }
            else if (!flag_query)
            {
                Console.WriteLine("兩秒後回主畫面");
                System.Threading.Thread.Sleep(2000);
            }
            st.Clear();
            return flag;
        }
        public static void show_list(IEnumerable<Station> query, List<Station> st)
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
        public static void show_detail(List<Station> st, string choose_dn)
        {
            int a = Convert.ToInt16(choose_dn);
            if (a != 0) { 
                a--;
                if (a < st.Count)
                {
                    Console.Clear();
                    Console.WriteLine(" 測  站  名  稱 ： {0}", st[a].SiteName);
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
}
