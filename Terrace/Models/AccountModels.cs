using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Terrace.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "请选择注册身份！")]
        [DisplayName("会员身份")]
        public Roles MemberRole { get; set; }

        [DisplayName("会员账号")]
        [Description("以Email当成会员登陆帐号")]
        [Required(ErrorMessage = "请输入Email地址")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(250, ErrorMessage = "Email长度无法超过250个字符")]
        public string Email { get; set; }

        [DisplayName("会员密码")]
        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("中文姓名")]
        [Required(ErrorMessage = "请输入中文姓名")]
        [MaxLength(5, ErrorMessage = "中文姓名不得超过5个字")]
        [Description("暂不考虑外国人用英文注册会员的情况")]
        public string Name { get; set; }

        [DisplayName("会员注册时间")]
        public DateTime RegisterOn { get; set; }
    }
    public enum Roles { 学生 = 1, 教师 }
    public class Student : Member
    {
        public virtual List<Exam> Exams { get; set; }
        public virtual List<Evaluation> Evaluations { get; set; }
    }
    public class Teacher : Member
    {
        public virtual List<Paper> Papers { get; set; }
        public virtual List<Evaluation> Evaluations { get; set; }

    }
    public class MemberLoginViewModel
    {
        [Required(ErrorMessage = "请选择你的身份！")]
        [DisplayName("你的身份")]
        public Roles role { get; set; }

        [DisplayName("你的账号名")]
        [DataType(DataType.EmailAddress, ErrorMessage = "请输入您的Email地址")]
        [Required(ErrorMessage = "请输入{0}")]
        public string email { get; set; }

        [DisplayName("你的密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请输入{0}")]
        public string password { get; set; }
    }

    public class Administrator
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("管理员账号")]
        [Required(ErrorMessage = "请输入管理员账号名")]
        [MaxLength(250, ErrorMessage = "不得超过250个字")]
        public string Name { get; set; }

        [DisplayName("管理员密码")]
        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("管理员注册时间")]
        public DateTime RegisterOn { get; set; }
    }
    public class AdminLoginViewModel
    {
        [DisplayName("会员账号")]
        [DataType(DataType.EmailAddress, ErrorMessage = "请输入您的Email地址")]
        [Required(ErrorMessage = "请输入{0}")]
        public string name { get; set; }
        [DisplayName("会员密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请输入{0}")]
        public string password { get; set; }
    }
}