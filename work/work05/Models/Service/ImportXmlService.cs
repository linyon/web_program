using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using YON.Models;
namespace YON.Service
{
    public class ImportXmlService
    {
        public List<Station> findst(string XmlPath)
        {
            List<Station> st = new List<Station>();
            var xml = XElement.Load(XmlPath);
            var sts_node = xml.Descendants("Data").ToList();
            sts_node.Where(x => !x.IsEmpty).ToList().ForEach(st_node =>
            {
                var SiteName = st_node.Element("SiteName").Value.Trim();
                var UVI = st_node.Element("UVI").Value.Trim();
                var PublishAgency = st_node.Element("PublishAgency").Value.Trim();
                var County = st_node.Element("County").Value.Trim();
                var WGS84Lon = st_node.Element("WGS84Lon").Value.Trim();
                var WGS84Lat = st_node.Element("WGS84Lat").Value.Trim();
                var PublishTime = st_node.Element("PublishTime").Value.Trim();
                Station st_data = new Station();
                st_data.SiteName = SiteName;
                st_data.UVI = int.Parse(UVI);
                st_data.PublishAgency = PublishAgency;
                st_data.County = County;
                st_data.WGS84Lon = WGS84Lon;
                st_data.WGS84Lat = WGS84Lat;
                st_data.PublishTime = DateTime.Parse(PublishTime);
                st.Add(st_data);
            });
            return st;
        }
        public List<Record> findrd_jsonUrl()
        {
            List<Record> rd = new List<Record>();
            var db = new YON.Repository.StationRepository();
            var rd_Db = new YON.Repository.RecordRepository();
            var URL = @"http://opendata.epa.gov.tw/ws/Data/UV/?format=json";
            var json_str = "";
            using (var web_client = new System.Net.WebClient())
            {
                web_client.Encoding = Encoding.UTF8;
                json_str = web_client.DownloadString(URL);
            }
            var stations = db.FindAllSt()
                .ToDictionary(x => x.SiteName, x => x);
            var json = JsonConvert.DeserializeObject<JArray>(json_str); //JSON字串還原回JObject，動態存取      
            var jsonDatas = json.ToList();//json.Properties().Values().ToList();
            jsonDatas.ForEach(item =>
            {
                var rd_Array = item as JObject;
                var st_sitename = rd_Array.Property("SiteName").Value.ToString().Trim();
                if (!stations.ContainsKey(st_sitename))
                {
                    return;
                }
                var station = stations[st_sitename];
                var pt = rd_Array.Property("PublishTime").Value.ToString().Trim();
                var uvi = rd_Array.Property("UVI").Value.ToString().Trim();
                var publish_time = DateTime.Parse(pt);


                Record new_rd = new Record();
                new_rd.SiteName = station.SiteName;
                new_rd.UVI = int.Parse(uvi);
                new_rd.PublishTime = publish_time;
                var isExist = rd_Db.isExist(new_rd);
                //rd.Add(new_rd);
                if (!isExist)
                {
                    station.LastRecordTime = new_rd.PublishTime;
                    station.LastRecordUVI = new_rd.UVI;
                    new_rd.Station = station;
                    rd.Add(new_rd);
                }

            });
            return rd;
        }
        public List<Record> createrd_jsonUrl()
        {
            List<Record> rd = new List<Record>();
            var db = new YON.Repository.StationRepository();
            var rd_Db = new YON.Repository.RecordRepository();
            var URL = @"http://opendata.epa.gov.tw/ws/Data/UV/?format=json";
            var json_str = "";
            using (var web_client = new System.Net.WebClient())
            {
                web_client.Encoding = Encoding.UTF8;
                json_str = web_client.DownloadString(URL);
            }
            var stations = db.FindAllSt()
                .ToDictionary(x => x.SiteName, x => x);
            var json = JsonConvert.DeserializeObject<JArray>(json_str); //JSON字串還原回JObject，動態存取      
            var jsonDatas = json.ToList();//json.Properties().Values().ToList();
            jsonDatas.ForEach(item =>
            {
                var rd_Array = item as JObject;
                var st_sitename = rd_Array.Property("SiteName").Value.ToString().Trim();
                if (!stations.ContainsKey(st_sitename))
                {
                    return;
                }
                var station = stations[st_sitename];
                var pt = rd_Array.Property("PublishTime").Value.ToString().Trim();
                var uvi = rd_Array.Property("UVI").Value.ToString().Trim();
                var publish_time = DateTime.Parse(pt);


                Record new_rd = new Record();
                new_rd.SiteName = station.SiteName;
                new_rd.UVI = int.Parse(uvi);
                new_rd.PublishTime = publish_time;
                var isExist = rd_Db.isExist(new_rd);
                //rd.Add(new_rd);
                if (!isExist) //是否存在
                {
                    station.LastRecordTime = new_rd.PublishTime;
                    station.LastRecordUVI = new_rd.UVI;
                    //new_rd.Station = station;
                    db.UpdateLastRecord(station);
                    rd.Add(new_rd);
                }

            });
            rd_Db.Create(rd);
            return rd;

        }
    }
}
