﻿using System;
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
            var sh = new YON.Show.Show();          
            var list = db.FindAllSt();
            if ( list.Count == 0) //第一次載入xml，存入db
            {
                var sts = import.findst(@"D:\Work\Web\web_program\work\work02\uv.xml");
                Console.WriteLine("第一次載入xml，存入db");
                sts.ToList().ForEach(x => { db.Create(x); });
                list = db.FindAllSt();
            }
            Console.WriteLine(string.Format("共收到{0}筆紫外線監測資料", list.Count));
            Console.WriteLine("==========================================");
            Console.WriteLine("（1）依測站查詢（2）依發布機關查詢（3）全部列表（0）離開");
            Console.WriteLine("==========================================");
            Console.Write("請選擇列表方式：");
            var choose = Console.ReadLine();
            while (sh.ShowSt(list, choose))
            {
                Console.Clear();
                Console.WriteLine(string.Format("共收到{0}筆紫外線監測資料", list.Count));
                Console.WriteLine("==========================================");
                Console.WriteLine("（1）依測站查詢（2）依發布機關查詢（3）全部列表（0）離開");
                Console.WriteLine("==========================================");
                Console.Write("請選擇列表方式：");
                choose = Console.ReadLine();
            }
        }
        
    }
}
