﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YON.Models;
using System.IO;

namespace YON
{
    class Program
    {
        static void setDBFilePath()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relative = @"..\..\..\Web\App_Data\";
            string absolute = Path.GetFullPath(Path.Combine(baseDirectory, relative));
            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);
        }
        private static void createRecord()
        {
            Service.ImportXmlService import = new Service.ImportXmlService();
            var records = import.createrd_jsonUrl();
            //Service.ImportXmlService import = new Service.ImportXmlService();
            //var records = import.findrd_jsonUrl();
            //Repository.RecordRepository db = new Repository.RecordRepository();
            //db.Create(records);
            //Repository.StationRepository st_Dd = new Repository.StationRepository();
            //records.ForEach(x =>
            //{
            //    var st = x.Station;
            //    var list = new List<Station>();
            //    list.Add(st);
            //    st_Dd.Create(new List<Station> { st });
            //});
        }
        static void Main(string[] args)
        {
            setDBFilePath();
            createRecord();
            Console.WriteLine("建立Record完成");
            Console.ReadKey();
            //var import = new YON.Service.ImportXmlService();
            //var db = new YON.Repository.StationRepository();
            //var sh = new YON.Show.Show();
            //var list = db.FindAllSt();
            //if ( list.Count == 0) //第一次載入xml，存入db
            //{
            //    Console.WriteLine("第一次載入xml，存入db");
            //    var sts = import.findst(@"D:\Work\Web\web_program\work\work03\uv.xml");
            //    sts.ToList().ForEach(x => { db.Create(x); });
            //    list = db.FindAllSt();
            //}
            //Console.WriteLine(string.Format("共收到{0}筆紫外線監測資料", list.Count));
            //Console.WriteLine("==========================================");
            //Console.WriteLine("（1）依測站查詢（2）依發布機關查詢（3）全部列表（0）離開");
            //Console.WriteLine("==========================================");
            //Console.Write("請選擇列表方式：");
            //var choose = Console.ReadLine();
            //while (sh.ShowSt(list, choose))
            //{
            //    Console.Clear();
            //    Console.WriteLine(string.Format("共收到{0}筆紫外線監測資料", list.Count));
            //    Console.WriteLine("==========================================");
            //    Console.WriteLine("（1）依測站查詢（2）依發布機關查詢（3）全部列表（0）離開");
            //    Console.WriteLine("==========================================");
            //    Console.Write("請選擇列表方式：");
            //    choose = Console.ReadLine();
            //}
        }
        
    }
}
