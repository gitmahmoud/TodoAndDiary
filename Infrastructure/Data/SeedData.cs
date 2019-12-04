using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Infrastructure.Data.UnitOfWork;
using Domain.Aggregates;

namespace Infrastructure.Data
{
    public static class DbDataInitializier
    {
        public static void InitializeDatabase()
        {
            // Database.SetInitializer(new SeedData());

            using (var ctx = new MainUnitOfWork())
            {
                if (ctx.Database.Exists())
                    return;

                var diary1 = new Diary() {Text = "diary 01"};
                var diary2 = new Diary() {Text = "diary 02"};
                var diary3 = new Diary() {Text = "diary 03"};
                var diary4 = new Diary() {Text = "diary 04"};
                var diary5 = new Diary() {Text = "diary 05"};
                var diary6 = new Diary() {Text = "diary 06"};
                var diary7 = new Diary() {Text = "diary 07"};
                var diary8 = new Diary() {Text = "diary 08"};

                ctx.Diaries.Add(diary1);
                ctx.Diaries.Add(diary2);
                ctx.Diaries.Add(diary3);
                ctx.Diaries.Add(diary4);
                ctx.Diaries.Add(diary5);
                ctx.Diaries.Add(diary6);
                ctx.Diaries.Add(diary7);
                ctx.Diaries.Add(diary8);

                var todo1 = new Todo() { Text = "todo 01" ,  DueDate = DateTime.Now };
                var todo2 = new Todo() { Text = "todo 02" , DueDate = DateTime.Now};
                var todo3 = new Todo() { Text = "todo 03" , DueDate = DateTime.Now};
                var todo4 = new Todo() { Text = "todo 04" , DueDate = DateTime.Now};
                var todo5 = new Todo() { Text = "todo 05" , DueDate = DateTime.Now};
                var todo6 = new Todo() { Text = "todo 06" , DueDate = DateTime.Now};
                var todo7 = new Todo() { Text = "todo 07" , DueDate = DateTime.Now};
                var todo8 = new Todo() { Text = "todo 08" , DueDate = DateTime.Now};

                ctx.Todos.Add(todo1);
                ctx.Todos.Add(todo2);
                ctx.Todos.Add(todo3);
                ctx.Todos.Add(todo4);
                ctx.Todos.Add(todo5);
                ctx.Todos.Add(todo6);
                ctx.Todos.Add(todo7);
                ctx.Todos.Add(todo8);

                ctx.SaveChanges();
            }
        }
    }

    //public class SeedData : CreateDatabaseIfNotExists<MainUnitOfWork>
    //{
    //    protected override void Seed(MainUnitOfWork context)
    //    {
    //        var diary01 = new Diary(){ Text = "Diary text", CreationDate = DateTime.Now };
    //        context.Diaries.Add(diary01);

    //        var todo01 = new Todo() { Text = "Todo text", CreationDate = DateTime.Now , DueDate = DateTime.Now.AddDays(3)};
    //        context.Todos.Add(todo01);

    //        context.SaveChanges();
    //    }
    //}
}