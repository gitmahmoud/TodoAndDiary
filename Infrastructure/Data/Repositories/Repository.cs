using Domain;
using Domain.BaseTypes;
using Domain.Interfaces;
using Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected IQueryableUnitOfWork _unitOfWork;

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
            set { _unitOfWork = (IQueryableUnitOfWork)value; }
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        public virtual void Add(TEntity item)
        {
            if (item == null) throw new ArgumentNullException("item");

            GetSet().Add(item);
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        public virtual void Remove(TEntity item)
        {
            if (item == null) throw new ArgumentNullException("item");

            _unitOfWork.Attach(item);
            GetSet().Remove(item);
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        public virtual void TrackItem(TEntity item)
        {
            if (item == null) throw new ArgumentNullException("item");

            _unitOfWork.Attach(item);
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        public virtual void Modify(TEntity item)
        {
            if (item == null) throw new ArgumentNullException("item");

            _unitOfWork.SetModified(item);
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="id"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public virtual TEntity Get(int id)
        {
            return GetSet().Find(id);
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return GetSet();
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="objectGraph"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> GetAllIncluding(string objectGraph)
        {
            return GetSet(objectGraph);
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="entityId"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="objectGraphs"></param>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public virtual TEntity GetIncluding(int entityId, List<string> objectGraphs)
        {
            if (objectGraphs == null || !objectGraphs.Any()) throw new ArgumentException("ObjectGraph cannot be null or empty!");
            var set = GetSet().Include(objectGraphs.FirstOrDefault());
            foreach (var s in objectGraphs)
                set.Include(s);

            return set.SingleOrDefault(entity => entity.Id == entityId);
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="specification"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> AllMatching(ISpecification<TEntity> specification)
        {
            return GetSet().Where(specification.SatisfiedBy());
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="specification"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="objectGraphs"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public IEnumerable<TEntity> AllMatchingIncluding(ISpecification<TEntity> specification, List<string> objectGraphs)
        {
            if (objectGraphs == null || !objectGraphs.Any()) throw new ArgumentException("ObjectGraph cannot be null or empty!");
            var set = GetSet().Include(objectGraphs.FirstOrDefault());
            foreach (var s in objectGraphs)
                set.Include(s);

            return set.Where(specification.SatisfiedBy());
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <typeparam name="TKProperty"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></typeparam>
        /// <param name="specification"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="pageIndex"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="pageCount"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="orderByExpression"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="ascending"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public IEnumerable<TEntity> AllMatchingPaged<TKProperty>(ISpecification<TEntity> specification, int pageIndex, int pageCount,
            Expression<Func<TEntity, TKProperty>> orderByExpression, bool @ascending)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(String.Format("Page Index < 0 or Page Count <= 0; PageIndex = {0}, PageCount = {1}", pageIndex, pageCount));

            var set = GetSet().Where(specification.SatisfiedBy());

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            return set.OrderByDescending(orderByExpression)
                .Skip(pageCount * pageIndex)
                .Take(pageCount);
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="pageIndex"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="pageCount"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="orderByExpression"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="ascending"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> GetPaged<TKProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, TKProperty>> orderByExpression, bool ascending)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(String.Format("Page Index < 0 or Page Count <= 0; PageIndex = {0}, PageCount = {1}", pageIndex, pageCount));

            var set = GetSet();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            return set.OrderByDescending(orderByExpression)
                .Skip(pageCount * pageIndex)
                .Take(pageCount);
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="filter"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="pageIndex"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="pageCount"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="orderByExpression"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="ascending"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public IEnumerable<TEntity> GetPagedFiltered<TKProperty>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageCount, Expression<Func<TEntity, TKProperty>> orderByExpression,
            bool @ascending)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(String.Format("Page Index < 0 or Page Count <= 0; PageIndex = {0}, PageCount = {1}", pageIndex, pageCount));

            var set = GetSet().Where(filter);

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            return set.OrderByDescending(orderByExpression)
                .Skip(pageCount * pageIndex)
                .Take(pageCount);
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="filter"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="pageIndex"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="pageCount"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="orderByExpression"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="ascending"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="objectGraphs"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public IEnumerable<TEntity> GetPagedFilteredIncluding<TKProperty>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageCount,
            Expression<Func<TEntity, TKProperty>> orderByExpression, bool @ascending, List<string> objectGraphs)
        {

            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(String.Format("Page Index < 0 or Page Count <= 0; PageIndex = {0}, PageCount = {1}", pageIndex, pageCount));
            if (objectGraphs == null || !objectGraphs.Any()) throw new ArgumentException("ObjectGraph cannot be null or empty!");

            var set = GetSet().Include(objectGraphs.FirstOrDefault());
            foreach (var s in objectGraphs)
                set.Include(s);

            if (ascending)
            {
                return set.Where(filter).OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            return set.Where(filter).OrderByDescending(orderByExpression)
                .Skip(pageCount * pageIndex)
                .Take(pageCount);
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="filter"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter)
        {
            return GetSet().Where(filter);
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="filter"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="objectGraphs"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> GetFilteredIncluding(Expression<Func<TEntity, bool>> filter, List<string> objectGraphs)
        {
            if (objectGraphs == null || !objectGraphs.Any()) throw new ArgumentException("ObjectGraph cannot be null or empty!");
            var set = GetSet().Include(objectGraphs.FirstOrDefault());
            foreach (var s in objectGraphs)
                set.Include(s);

            return set.Where(filter);
        }


        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public int GetCount()
        {
            return GetSet().Count();
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="filter"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public int GetCount(Expression<Func<TEntity, bool>> filter)
        {
            return GetSet().Count(filter);
        }

        /// <summary>
        /// <see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="persisted"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <param name="current"><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></param>
        /// <returns><see cref="CommPlat.Core.Domain.Interfaces.IRepository{TValueObject}"/></returns>
        public virtual void Merge(TEntity persisted, TEntity current)
        {
            _unitOfWork.ApplyCurrentValues(persisted, current);
        }

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }

        /// <summary>
        /// Returns a dbset for an entity
        /// </summary>
        /// <returns>Entity dbset</returns>
        protected IDbSet<TEntity> GetSet()
        {
            return _unitOfWork.CreateSet<TEntity>();
        }

        /// <summary>
        /// Returns dbset of entity including an object graph
        /// </summary>
        /// <param name="objectGraph">Object graph</param>
        /// <returns>Entity enumerable list</returns>
        IEnumerable<TEntity> GetSet(string objectGraph)
        {
            return _unitOfWork.CreateSet<TEntity>(objectGraph);
        }
    }
}
