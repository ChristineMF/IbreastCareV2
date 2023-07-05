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
        [DisplayName("手術日")]
        public Nullable<System.DateTime> OperationDate { get; set; }
        [DisplayName("身高")]
        public Nullable<double> Height { get; set; }
        [DisplayName("停經")]
        public string Menopause { get; set; }
        [DisplayName("細胞")]
        public string CellType { get; set; }
        [DisplayName("ER")]
        public string ER { get; set; }
        [DisplayName("PR")]
        public string PR { get; set; }
        [DisplayName("HER-2/neu")]
        public string Her { get; set; }
        [DisplayName("手術")]
        public string OperationType { get; set; }
        [DisplayName("療程")]
        public string TreatPlan { get; set; }
        [DisplayName("備註")]
        public string Note { get; set; }
        [DisplayName("建立日期")]
        public System.DateTime InputDate { get; set; }
        [DisplayName("UserID")]
        public int ? UserId { get; set; }
    }
}