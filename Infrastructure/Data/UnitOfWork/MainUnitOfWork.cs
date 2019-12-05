using Infrastructure.Data.Interfaces;
using Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.UnitOfWork
{
    public class MainUnitOfWork : DbContext, IQueryableUnitOfWork
    {

        /// <summary>
        /// Constructor specifying the name of the Db to be used by the Entity Framework
        /// </summary>
        
        public MainUnitOfWork() : base("name=ToDoAndDiary") {

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainUnitOfWork, Infrastructure.Migrations.Configuration>());

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Todo> Todos { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        

        public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public IEnumerable<TEntity> CreateSet<TEntity>(string objectGraph) where TEntity : class
        {
            return Set<TEntity>().Include(objectGraph);
        }

        public IEnumerable<TEntity> CreateSet<TEntity>(int entityId, string objectGraph1, string objectGraph2) where TEntity : class
        {
            return Set<TEntity>().Include(objectGraph1).Include(objectGraph2);
        }

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            //attach and set as unchanged
            if (Entry(item).State == EntityState.Detached)
                Entry(item).State = EntityState.Unchanged;
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            //this operation also attach item in object state manager
            Entry(item).State = EntityState.Modified;
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            //if it is not attached, attach original and set current values
            Entry(original).CurrentValues.SetValues(current);
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed;
            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);
        }

        public void RollbackChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        {
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        }
                    case EntityState.Deleted:
                        {
                            entry.State = EntityState.Unchanged;
                            break;
                        }
                    case EntityState.Added:
                        {
                            entry.State = EntityState.Detached;
                            break;
                        }
                }
            }
        }
    }
}