using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work_0317_1
{
    class station
    {
        public string SiteName { get; set; } //測站名稱
        public string UVI { get; set; } //紫外線指數
        public string PublishAgency { get; set; } //發布機關
        public string County { get; set; } //縣市
        public string WGS84Lon { get; set; } //經度
        public string WGS84Lat { get; set; } //緯度
        public string PublishTime { get; set; } //發布時間

    }
    
}
