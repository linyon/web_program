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
    }
}
