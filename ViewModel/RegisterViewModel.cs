using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IbreastCare.ViewModel
{
    public class RegisterViewModel
    {
        public int UserId { get; set; }

        
        [DisplayName("姓名")]
        [Required(ErrorMessage = "請輸入姓名")]
        public string RealName { get; set; }

        [DisplayName("暱稱")]
        public string NickName { get; set; }

       
        [DisplayName("帳號")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string UserName { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("確認密碼")]
        [Required(ErrorMessage = "請輸入確認密碼")]
        [Compare("Password", ErrorMessage = "密碼和確認密碼不相符")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [DisplayName("手機")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "請輸入手機號碼")]
        public string CellPhone { get; set; }

        [DisplayName("Email")]
        [RegularExpression("^.+@[^\\.].*\\.[a-z]{2,}$", ErrorMessage = "請輸入正確的Email")]
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

        
    }
}