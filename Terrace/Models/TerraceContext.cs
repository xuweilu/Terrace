using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Terrace.DataAccess;

namespace Terrace.Models
{
    public class TerraceContext : DbContext
    {
        public TerraceContext() : base("name=DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ExamMap());
            modelBuilder.Configurations.Add(new PaperMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new TeacherMap());
            modelBuilder.Configurations.Add(new SingleQuestionMap());
            modelBuilder.Configurations.Add(new MultipleQuestionMap());
        }

        public DbSet<Paper> Papers { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<SingleAnswer> SingleAnswers { get; set; }
        public DbSet<TrueOrFalseAnswer> TrueOrFalseAnswers { get; set; }
        public DbSet<MultipleAnswer> MultipleAnswers { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<TrueOrFalseQuestion> TrueOrFalseQuestions { get; set; }
        public DbSet<SingleQuestion> SingleQuestions { get; set; }
        public DbSet<MultipleQuestion> MultipleQuestions { get; set; }
        public DbSet<SingleOption> SingleOptions { get; set; }
        public DbSet<MultipleOption> MultipleOptions { get; set; }

    }
}