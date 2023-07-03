using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IbreastCare.ViewModel
{
    public class PatientViewModel
    {
        [DisplayName("ID")]
        public int UserId { get; set; }

        [DisplayName("姓名")]
        public string RealName { get; set; }


        [DisplayName("暱稱")]
        public string NickName { get; set; }

        [DisplayName("帳號")]
        public string UserName { get; set; }

        [DisplayName("手機")]
        public string CellPhone { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("出生日期")]
        //public Nullable<System.DateTime> BirthDate { get; set; }
        public DateTime BirthDate { get; set; }

        [DisplayName("年齡")]
        public int age { get; set; }

        [DisplayName("註冊日期")]
        public System.DateTime InputDate { get; set; }

       

        
    }
}