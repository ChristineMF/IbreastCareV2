using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IbreastCare.ViewModel
{
    public class AccountViewModel
    {
        [DisplayName("ID")]
        public int UserId { get; set; }

        
        [DisplayName("姓名")]
        public string RealName { get; set; }

        [DisplayName("暱稱")]
        public string NickName { get; set; }

       
        [DisplayName("帳號")]
        public string UserName { get; set; }

        [DisplayName("密碼")]
        public string Password { get; set; }


        [DisplayName("手機")]
        public string CellPhone { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("出生日期")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [DisplayName("註冊日期")]
        public System.DateTime InputDate { get; set; }

        [DisplayName("角色Id")]
        public int RoleId { get; set; }

        [DisplayName("權限狀態")]
        public string Status { get; set; }

        [DisplayName("設定權限者")]
        public string SettingUser { get; set; }

        [DisplayName("設定權限日期")]
        public Nullable<System.DateTime> SettingDate { get; set; }

    }
}