namespace Terrace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teachers", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Password", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Teachers", "Password", c => c.String(nullable: false, maxLength: 40));
        }
    }
}
