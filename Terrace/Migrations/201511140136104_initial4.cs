namespace Terrace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Exams", "Id", "dbo.Papers");
            DropForeignKey("dbo.MultipleAnswers", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.SingleAnswers", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.TrueOrFalseAnswers", "ExamId", "dbo.Exams");
            DropIndex("dbo.Exams", new[] { "Id" });
            DropPrimaryKey("dbo.Exams");
            AddColumn("dbo.Exams", "PaperId", c => c.Int(nullable: false));
            AlterColumn("dbo.Exams", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Exams", "Id");
            CreateIndex("dbo.Exams", "PaperId");
            AddForeignKey("dbo.Exams", "PaperId", "dbo.Papers", "Id");
            AddForeignKey("dbo.MultipleAnswers", "ExamId", "dbo.Exams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SingleAnswers", "ExamId", "dbo.Exams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TrueOrFalseAnswers", "ExamId", "dbo.Exams", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrueOrFalseAnswers", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.SingleAnswers", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.MultipleAnswers", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Exams", "PaperId", "dbo.Papers");
            DropIndex("dbo.Exams", new[] { "PaperId" });
            DropPrimaryKey("dbo.Exams");
            AlterColumn("dbo.Exams", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Exams", "PaperId");
            AddPrimaryKey("dbo.Exams", "Id");
            CreateIndex("dbo.Exams", "Id");
            AddForeignKey("dbo.TrueOrFalseAnswers", "ExamId", "dbo.Exams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SingleAnswers", "ExamId", "dbo.Exams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MultipleAnswers", "ExamId", "dbo.Exams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Exams", "Id", "dbo.Papers", "Id");
        }
    }
}
