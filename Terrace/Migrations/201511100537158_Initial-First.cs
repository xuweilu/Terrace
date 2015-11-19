namespace Terrace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialFirst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Password = c.String(nullable: false, maxLength: 40),
                        RegisterOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Evaluations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        EvaluatedTeacherId = c.Int(nullable: false),
                        StudentEvaluatingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.EvaluatedTeacherId)
                .ForeignKey("dbo.Students", t => t.StudentEvaluatingId)
                .Index(t => t.EvaluatedTeacherId)
                .Index(t => t.StudentEvaluatingId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberRole = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 250),
                        Password = c.String(nullable: false, maxLength: 40),
                        Name = c.String(nullable: false, maxLength: 5),
                        RegisterOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Papers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EditOn = c.DateTime(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Papers", t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.Id)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.MultipleAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.Int(nullable: false),
                        ExamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.ExamId, cascadeDelete: true)
                .Index(t => t.ExamId);
            
            CreateTable(
                "dbo.SingleAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.Int(nullable: false),
                        ExamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.ExamId, cascadeDelete: true)
                .Index(t => t.ExamId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberRole = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 250),
                        Password = c.String(nullable: false, maxLength: 40),
                        Name = c.String(nullable: false, maxLength: 5),
                        RegisterOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrueOrFalseAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.Boolean(nullable: false),
                        ExamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.ExamId, cascadeDelete: true)
                .Index(t => t.ExamId);
            
            CreateTable(
                "dbo.MultipleQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        PaperId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Papers", t => t.PaperId)
                .Index(t => t.PaperId);
            
            CreateTable(
                "dbo.MultipleOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OptionId = c.Int(nullable: false),
                        OptionProperty = c.String(nullable: false),
                        Answer = c.Boolean(nullable: false),
                        MultipleQuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MultipleQuestions", t => t.MultipleQuestionId, cascadeDelete: true)
                .Index(t => t.MultipleQuestionId);
            
            CreateTable(
                "dbo.SingleQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        PaperId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Papers", t => t.PaperId)
                .Index(t => t.PaperId);
            
            CreateTable(
                "dbo.SingleOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OptionId = c.Int(nullable: false),
                        OptionProperty = c.String(nullable: false),
                        SingleQuesionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SingleQuestions", t => t.SingleQuesionId, cascadeDelete: true)
                .Index(t => t.SingleQuesionId);
            
            CreateTable(
                "dbo.TrueOrFalseQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        PaperId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Papers", t => t.PaperId)
                .Index(t => t.PaperId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Papers", "AuthorId", "dbo.Teachers");
            DropForeignKey("dbo.TrueOrFalseQuestions", "PaperId", "dbo.Papers");
            DropForeignKey("dbo.SingleQuestions", "PaperId", "dbo.Papers");
            DropForeignKey("dbo.SingleOptions", "SingleQuesionId", "dbo.SingleQuestions");
            DropForeignKey("dbo.MultipleQuestions", "PaperId", "dbo.Papers");
            DropForeignKey("dbo.MultipleOptions", "MultipleQuestionId", "dbo.MultipleQuestions");
            DropForeignKey("dbo.TrueOrFalseAnswers", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Exams", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Evaluations", "StudentEvaluatingId", "dbo.Students");
            DropForeignKey("dbo.SingleAnswers", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Exams", "Id", "dbo.Papers");
            DropForeignKey("dbo.MultipleAnswers", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Evaluations", "EvaluatedTeacherId", "dbo.Teachers");
            DropIndex("dbo.TrueOrFalseQuestions", new[] { "PaperId" });
            DropIndex("dbo.SingleOptions", new[] { "SingleQuesionId" });
            DropIndex("dbo.SingleQuestions", new[] { "PaperId" });
            DropIndex("dbo.MultipleOptions", new[] { "MultipleQuestionId" });
            DropIndex("dbo.MultipleQuestions", new[] { "PaperId" });
            DropIndex("dbo.TrueOrFalseAnswers", new[] { "ExamId" });
            DropIndex("dbo.SingleAnswers", new[] { "ExamId" });
            DropIndex("dbo.MultipleAnswers", new[] { "ExamId" });
            DropIndex("dbo.Exams", new[] { "StudentId" });
            DropIndex("dbo.Exams", new[] { "Id" });
            DropIndex("dbo.Papers", new[] { "AuthorId" });
            DropIndex("dbo.Evaluations", new[] { "StudentEvaluatingId" });
            DropIndex("dbo.Evaluations", new[] { "EvaluatedTeacherId" });
            DropTable("dbo.TrueOrFalseQuestions");
            DropTable("dbo.SingleOptions");
            DropTable("dbo.SingleQuestions");
            DropTable("dbo.MultipleOptions");
            DropTable("dbo.MultipleQuestions");
            DropTable("dbo.TrueOrFalseAnswers");
            DropTable("dbo.Students");
            DropTable("dbo.SingleAnswers");
            DropTable("dbo.MultipleAnswers");
            DropTable("dbo.Exams");
            DropTable("dbo.Papers");
            DropTable("dbo.Teachers");
            DropTable("dbo.Evaluations");
            DropTable("dbo.Administrators");
        }
    }
}
