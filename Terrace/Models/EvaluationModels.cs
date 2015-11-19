using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Terrace.Models
{
    public class Evaluation :IEntityObjectState
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public virtual Teacher EvaluatedTeacher { get; set; }
        public virtual Student StudentEvaluating { get; set; }
        [NotMapped]
        public EntityObjectState ObjectState
        {
            get;
            set;
        }
    }
}