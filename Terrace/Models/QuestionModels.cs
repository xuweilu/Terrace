using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Terrace.Models
{
    public abstract class Question
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("题目类型")]
        public QuestionType Type { get; set; }
        [DisplayName("题目内容")]
        [Required(ErrorMessage = "请输入题目内容！")]
        public string Content { get; set; }
    }
    public enum QuestionType { 判断题 = 1, 单选题, 多选题 };
    public class TrueOrFalseQuestion : Question,IEntityObjectState
    {
        [DisplayName("正确答案")]
        [Required(ErrorMessage = "请选择题目正确答案！")]
        public bool Answer { get; set; }
        public virtual Paper Paper { get; set; }
        [NotMapped]
        public EntityObjectState ObjectState
        {
            get;
            set;
        }
    }
    public enum SingleOptionId { 选项A = 1, 选项B, 选项C, 选项D };
    public class SingleQuestion : Question,IEntityObjectState
    {
        [DisplayName("正确答案")]
        [Required(ErrorMessage = "请选择题目正确答案！")]
        public SingleOptionId Answer { get; set; }
        public virtual List<SingleOption> SingleOptions { get; set; }
        public virtual Paper Paper { get; set; }

        [NotMapped]
        public EntityObjectState ObjectState
        {
            get;
            set;
        }
    }
    public class SingleOption : IEntityObjectState
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("选项号")]
        public SingleOptionId OptionId { get; set; }
        [DisplayName("选项内容")]
        [Required(ErrorMessage = "请输入选项内容！")]
        public string OptionProperty { get; set; }
        public virtual SingleQuestion SingleQuestion { get; set; }
        [NotMapped]
        public EntityObjectState ObjectState
        {
            get;
            set;
        }
    }
    public class MultipleQuestion : Question,IEntityObjectState
    {
        public virtual Paper Paper { get; set; }
        public virtual List<MultipleOption> MultipleOptions { get; set; }
        [NotMapped]
        public EntityObjectState ObjectState
        {
            get;
            set;
        }
    }
    public enum MultipleOptionId { 选项A = 1, 选项B, 选项C, 选项D, 选项E, 选项F, 选项G, 选项H, 选项I, 选项J, 选项K, 选项L, 选项M, 选项N, 选项O, 选项P, 选项Q, 选项R, 选项S, 选项T, 选项U, 选项V, 选项W, 选项X, 选项Y, 选项Z };
    public class MultipleOption : IEntityObjectState
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("选项号")]
        public MultipleOptionId OptionId { get; set; }
        [DisplayName("选项内容")]
        [Required(ErrorMessage = "请输入选项内容！")]
        public string OptionProperty { get; set; }
        [DisplayName("选项是否正确？")]
        [Required(ErrorMessage = "请选择选项是否正确！")]
        public bool Answer { get; set; }
        public virtual MultipleQuestion MultipleQuestion { get; set; }
        [NotMapped]
        public EntityObjectState ObjectState
        {
            get;
            set;
        }
    }
}