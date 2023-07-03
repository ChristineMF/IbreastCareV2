using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace IbreastCare.ViewModel
{
    public class MydataViewModel
    {
        [DisplayName("我的ID")]
        public int MyId { get; set; }
        [DisplayName("手術日期")]
        public Nullable<System.DateTime> OperationDate { get; set; }
        [DisplayName("我的身高")]
        public Nullable<double> Height { get; set; }
        [DisplayName("是否停經")]
        public string Menopause { get; set; }
        [DisplayName("細胞類型")]
        public string CellType { get; set; }
        [DisplayName("ER")]
        public string ER { get; set; }
        [DisplayName("PR")]
        public string PR { get; set; }
        [DisplayName("HER-2/neu基因表現")]
        public string Her { get; set; }
        [DisplayName("手術型式")]
        public string OperationType { get; set; }
        [DisplayName("療程計畫")]
        public string TreatPlan { get; set; }
        [DisplayName("其他備註")]
        public string Note { get; set; }
        [DisplayName("建立日期")]
        public System.DateTime InputDate { get; set; }
        [DisplayName("ID")]
        public int UserId { get; set; }
    }
}