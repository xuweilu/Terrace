using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Terrace.Models;

namespace Terrace.DataAccess
{
    public class ExamMap : EntityTypeConfiguration<Exam>
    {
        public ExamMap()
        {
            //this.HasRequired(e => e.Paper).WithOptional(p => p.Exam).WillCascadeOnDelete(false);
            this.HasMany(e => e.SingleAnswers).WithRequired(s => s.Exam).Map(s => s.MapKey("ExamId"));
            this.HasMany(e => e.MultipleAnswers).WithRequired(m => m.Exam).Map(m => m.MapKey("ExamId"));
            this.HasMany(e => e.TrueOrFalseAnswers).WithRequired(t => t.Exam).Map(t => t.MapKey("ExamId"));
        }
    }
    public class PaperMap : EntityTypeConfiguration<Paper>
    {
        public PaperMap()
        {
            this.HasOptional(p => p.Exam).WithRequired(e => e.Paper).Map(e => e.MapKey("PaperId")).WillCascadeOnDelete(false);
            this.HasMany(p => p.TrueOrFalseQuestions).WithOptional(t => t.Paper).Map(t => t.MapKey("PaperId"));
            this.HasMany(p => p.SingleQuestions).WithOptional(s => s.Paper).Map(s => s.MapKey("PaperId"));
            this.HasMany(p => p.MultipleQuestions).WithOptional(m => m.Paper).Map(m => m.MapKey("PaperId"));
        }
    }
    public class EvaluationMap : EntityTypeConfiguration<Evaluation>
    {
        public EvaluationMap()
        {

        }
    }
    public class StudentMap : EntityTypeConfiguration<Student>
    {
        public StudentMap()
        {
            this.HasMany(s => s.Exams).WithRequired(e => e.Student).Map(e => e.MapKey("StudentId")).WillCascadeOnDelete(false);
            this.HasMany(s => s.Evaluations).WithRequired(e => e.StudentEvaluating).Map(e => e.MapKey("StudentEvaluatingId")).WillCascadeOnDelete(false);
        }
    }
    public class TeacherMap : EntityTypeConfiguration<Teacher>
    {
        public TeacherMap()
        {
            this.HasMany(t => t.Papers).WithRequired(p => p.Author).Map(p => p.MapKey("AuthorId")).WillCascadeOnDelete(false);
            this.HasMany(t => t.Evaluations).WithRequired(e => e.EvaluatedTeacher).Map(e => e.MapKey("EvaluatedTeacherId")).WillCascadeOnDelete(false);
        }
    }
    public class SingleQuestionMap : EntityTypeConfiguration<SingleQuestion>
    {
        public SingleQuestionMap()
        {
            this.HasMany(s => s.SingleOptions).WithRequired(s => s.SingleQuestion).Map(s => s.MapKey("SingleQuesionId"));
        }
    }
    public class MultipleQuestionMap : EntityTypeConfiguration<MultipleQuestion>
    {
        public MultipleQuestionMap()
        {
            this.HasMany(m => m.MultipleOptions).WithRequired(s => s.MultipleQuestion).Map(s => s.MapKey("MultipleQuestionId"));
        }
    }
}