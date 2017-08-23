namespace CodeFirstDB.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodeFirstDB.Homework10Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CodeFirstDB.Homework10Context";
        }

        protected override void Seed(CodeFirstDB.Homework10Context context)
        {
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

            context.Company.AddOrUpdate(
                    c => c.Id,
                    new Company { Name = "≤‚ ‘1", CreateTime = DateTime.Now, CreatorId = 1 },
                    new Company { Name = "≤‚ ‘2", CreateTime = DateTime.Now, CreatorId = 1 },
                    new Company { Name = "≤‚ ‘3", CreateTime = DateTime.Now, CreatorId = 1 },
                    new Company { Name = "≤‚ ‘4", CreateTime = DateTime.Now, CreatorId = 1 },
                    new Company { Name = "≤‚ ‘5", CreateTime = DateTime.Now, CreatorId = 1 },
                    new Company { Name = "≤‚ ‘6", CreateTime = DateTime.Now, CreatorId = 1 }
                );
        }
    }
}
