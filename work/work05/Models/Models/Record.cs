using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YON.Models
{
    public class Record
    {
        public int ID { get; set; } //測站名稱
        public string SiteName { get; set; } //測站名稱
        public string UVI { get; set; } //紫外線指數
        public DateTime PublishTime { get; set; } //發布時間
    }
}
