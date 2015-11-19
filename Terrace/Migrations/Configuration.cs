namespace Terrace.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Terrace.Models.TerraceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Terrace.Models.TerraceContext context)
        {
            //context.Students.AddOrUpdate(
            //    new Models.Student
            //    {
            //        MemberRole = (Models.Roles)1,
            //        Email = "xuweiluwx@live.com",
            //        Password = "wx3862174",
            //        Name = "知北游1",
            //        RegisterOn = DateTime.Now
            //    },
            //    new Models.Student
            //    {
            //        MemberRole = (Models.Roles)2,
            //        Email = "xuweiluwx@gmail.com",
            //        Password = "wx3862174",
            //        Name = "知北游2",
            //        RegisterOn = DateTime.Now
            //    },
            //    new Models.Student
            //    {
            //        MemberRole = (Models.Roles)3,
            //        Email = "xuweiluwx@qq.com",
            //        Password = "wx3862174",
            //        Name = "知北游3",
            //        RegisterOn = DateTime.Now
            //    }, new Models.Student
            //    {
            //        MemberRole = (Models.Roles)1,
            //        Email = "xuweiluwx@xx.com",
            //        Password = "wx3862174",
            //        Name = "知北游4",
            //        RegisterOn = DateTime.Now
            //    });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
