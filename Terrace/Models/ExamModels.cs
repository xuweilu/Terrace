using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Terrace.Models
{
    interface IEntityObjectState
    {
        EntityObjectState ObjectState { get; set; }
    }
    public enum EntityObjectState
    {
        Added,
        Modified,
        Deleted,
        Unchanged
    }
    public class Paper : IEntityObjectState
    {
        public int Id { get; set; }
        [DisplayName("出卷老师")]
        public virtual Teacher Author { get; set; }
        public virtual List<TrueOrFalseQuestion> TrueOrFalseQuestions { get; set; }
        public virtual List<SingleQuestion> SingleQuestions { get; set; }
        public virtual List<MultipleQuestion> MultipleQuestions { get; set; }
        [DisplayName("最近一次修改时间")]
        public DateTime EditOn { get; set; }
        public virtual Exam Exam { get; set; }
        [NotMapped]
        public EntityObjectState ObjectState
        {
            get;
            set;
        }
    }
    public class Exam : IEntityObjectState
    {
        public int Id { get; set; }
        [DisplayName("考试分数")]
        public int Score { get; set; }
        public virtual Student Student { get; set; }
        public virtual Paper Paper { get; set; }
        public virtual List<TrueOrFalseAnswer> TrueOrFalseAnswers { get; set; }
        public virtual List<SingleAnswer> SingleAnswers { get; set; }
        public virtual List<MultipleAnswer> MultipleAnswers { get; set; }
        [NotMapped]
        public EntityObjectState ObjectState
        {
            get;
            set;
        }

    }
    public class TrueOrFalseAnswer : IEntityObjectState
    {
        public int Id { get; set; }
        public bool Answer { get; set; }
        public virtual Exam Exam { get; set; }

        [NotMapped]
        public EntityObjectState ObjectState
        {
            get;
            set;
        }
    }
    public class SingleAnswer : IEntityObjectState
    {
        public int Id { get; set; }
        public SingleOptionId Answer { get; set; }
        public virtual Exam Exam { get; set; }
        [NotMapped]
        public EntityObjectState ObjectState
        {
            get;
            set;
        }
    }
    public class MultipleAnswer : IEntityObjectState
    {
        public int Id { get; set; }
        public MultipleOptionId Answer { get; set; }
        public virtual Exam Exam { get; set; }
        [NotMapped]
        public EntityObjectState ObjectState
        {
            get;
            set;
        }
    }
}