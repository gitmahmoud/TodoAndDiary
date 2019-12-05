namespace Infrastructure.Migrations
{
    using Domain.Aggregates;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.Data.UnitOfWork.MainUnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Infrastructure.Data.UnitOfWork.MainUnitOfWork";
        }

        protected override void Seed(Infrastructure.Data.UnitOfWork.MainUnitOfWork context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            context.Diaries.AddOrUpdate(
              p => p.Text,
              new Diary() { Text = "diary 01" },
              new Diary() { Text = "diary 02" },
              new Diary() { Text = "diary 03" },
              new Diary() { Text = "diary 04" },
              new Diary() { Text = "diary 05" },
              new Diary() { Text = "diary 06" },
              new Diary() { Text = "diary 07" },
              new Diary() { Text = "diary 08" }
            );

            context.Todos.AddOrUpdate(
              p => p.Text,
              new Todo() { Text = "todo 01", DueDate = DateTime.Now },
              new Todo() { Text = "todo 02", DueDate = DateTime.Now },
              new Todo() { Text = "todo 03", DueDate = DateTime.Now },
              new Todo() { Text = "todo 04", DueDate = DateTime.Now },
              new Todo() { Text = "todo 05", DueDate = DateTime.Now },
              new Todo() { Text = "todo 06", DueDate = DateTime.Now },
              new Todo() { Text = "todo 07", DueDate = DateTime.Now },
              new Todo() { Text = "todo 08", DueDate = DateTime.Now }
            );

            context.SaveChanges();
        }
    }
}