using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YON.Models
{
    public class Station
    {
        public int ID { get; set; } //測站ID
        public string SiteName { get; set; } //測站名稱
        public int UVI { get; set; } //紫外線指數
        public string PublishAgency { get; set; } //發布機關
        public string County { get; set; } //縣市
        public string WGS84Lon { get; set; } //經度
        public string WGS84Lat { get; set; } //緯度
        public DateTime PublishTime { get; set; } //發布時間
        public DateTime LastRecordTime { get; set; } //最後紀錄時間
        public int LastRecordUVI { get; set; } //最後紀錄UVI值
    }
}
