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

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
            set { _unitOfWork = (IQueryableUnitOfWork)value; }
        }
        public virtual void Add(TEntity item)
        {
            if (item == null) throw new ArgumentNullException("item");

            GetSet().Add(item);
        }

        public virtual void Remove(TEntity item)
        {
            if (item == null) throw new ArgumentNullException("item");

            _unitOfWork.Attach(item);
            GetSet().Remove(item);
        }


        public virtual void TrackItem(TEntity item)
        {
            if (item == null) throw new ArgumentNullException("item");

            _unitOfWork.Attach(item);
        }

        public virtual void Modify(TEntity item)
        {
            if (item == null) throw new ArgumentNullException("item");

            _unitOfWork.SetModified(item);
        }

        public virtual TEntity Get(int id)
        {
            return GetSet().Find(id);
        }

        public virtual TEntity GetIncluding(int entityId, List<string> objectGraphs)
        {
            if (objectGraphs == null || !objectGraphs.Any()) throw new ArgumentException("ObjectGraph cannot be null or empty!");
            var set = GetSet().Include(objectGraphs.FirstOrDefault());
            foreach (var s in objectGraphs)
                set.Include(s);

            return set.SingleOrDefault(entity => entity.Id == entityId);
        }


        public virtual IEnumerable<TEntity> GetAll()
        {
            return GetSet();
        }

        public virtual IEnumerable<TEntity> GetAllIncluding(string objectGraph)
        {
            return GetSet(objectGraph);
        }




        public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
        {
            IQueryable<TEntity> query = GetSet();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }









        //public IEnumerable<TEntity> GetAllFilteredOrdered<TKProperty>(ISpecification<TEntity> specification,
        //    Expression<Func<TEntity, TKProperty>> orderByExpression, bool @ascending)
        //{
        //    var set = GetSet().Where(specification.SatisfiedBy());

        //    if (ascending)
        //    {
        //        return set.OrderBy(orderByExpression);
        //    }
        //    return set.OrderByDescending(orderByExpression);
        //}


        //public virtual IEnumerable<TEntity> AllMatching(ISpecification<TEntity> specification)
        //{
        //    return GetSet().Where(specification.SatisfiedBy());
        //}


        //public IEnumerable<TEntity> AllMatchingIncluding(ISpecification<TEntity> specification, List<string> objectGraphs)
        //{
        //    if (objectGraphs == null || !objectGraphs.Any()) throw new ArgumentException("ObjectGraph cannot be null or empty!");
        //    var set = GetSet().Include(objectGraphs.FirstOrDefault());
        //    foreach (var s in objectGraphs)
        //        set.Include(s);

        //    return set.Where(specification.SatisfiedBy());
        //}


        //public IEnumerable<TEntity> AllMatchingPaged<TKProperty>(ISpecification<TEntity> specification, int pageIndex, int pageCount,
        //    Expression<Func<TEntity, TKProperty>> orderByExpression, bool @ascending)
        //{
        //    if (pageIndex < 0 || pageCount <= 0)
        //        throw new ArgumentException(String.Format("Page Index < 0 or Page Count <= 0; PageIndex = {0}, PageCount = {1}", pageIndex, pageCount));

        //    var set = GetSet().Where(specification.SatisfiedBy());

        //    if (ascending)
        //    {
        //        return set.OrderBy(orderByExpression)
        //                  .Skip(pageCount * pageIndex)
        //                  .Take(pageCount);
        //    }
        //    return set.OrderByDescending(orderByExpression)
        //        .Skip(pageCount * pageIndex)
        //        .Take(pageCount);
        //}


        //public virtual IEnumerable<TEntity> GetPaged<TKProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, TKProperty>> orderByExpression, bool ascending)
        //{
        //    if (pageIndex < 0 || pageCount <= 0)
        //        throw new ArgumentException(String.Format("Page Index < 0 or Page Count <= 0; PageIndex = {0}, PageCount = {1}", pageIndex, pageCount));

        //    var set = GetSet();

        //    if (ascending)
        //    {
        //        return set.OrderBy(orderByExpression)
        //                  .Skip(pageCount * pageIndex)
        //                  .Take(pageCount);
        //    }
        //    return set.OrderByDescending(orderByExpression)
        //        .Skip(pageCount * pageIndex)
        //        .Take(pageCount);
        //}

        //public IEnumerable<TEntity> GetPagedFiltered<TKProperty>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageCount, Expression<Func<TEntity, TKProperty>> orderByExpression,
        //    bool @ascending)
        //{
        //    if (pageIndex < 0 || pageCount <= 0)
        //        throw new ArgumentException(String.Format("Page Index < 0 or Page Count <= 0; PageIndex = {0}, PageCount = {1}", pageIndex, pageCount));

        //    var set = GetSet().Where(filter);

        //    if (ascending)
        //    {
        //        return set.OrderBy(orderByExpression)
        //                  .Skip(pageCount * pageIndex)
        //                  .Take(pageCount);
        //    }
        //    return set.OrderByDescending(orderByExpression)
        //        .Skip(pageCount * pageIndex)
        //        .Take(pageCount);
        //}

        //public IEnumerable<TEntity> GetPagedFilteredIncluding<TKProperty>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageCount,
        //   Expression<Func<TEntity, TKProperty>> orderByExpression, bool @ascending, List<string> objectGraphs)
        //{

        //    if (pageIndex < 0 || pageCount <= 0)
        //        throw new ArgumentException(String.Format("Page Index < 0 or Page Count <= 0; PageIndex = {0}, PageCount = {1}", pageIndex, pageCount));
        //    if (objectGraphs == null || !objectGraphs.Any()) throw new ArgumentException("ObjectGraph cannot be null or empty!");

        //    var set = GetSet().Include(objectGraphs.FirstOrDefault());
        //    foreach (var s in objectGraphs)
        //        set.Include(s);

        //    if (ascending)
        //    {
        //        return set.Where(filter).OrderBy(orderByExpression)
        //                  .Skip(pageCount * pageIndex)
        //                  .Take(pageCount);
        //    }
        //    return set.Where(filter).OrderByDescending(orderByExpression)
        //        .Skip(pageCount * pageIndex)
        //        .Take(pageCount);
        //}

        //public virtual IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter)
        //{
        //    return GetSet().Where(filter);
        //}

        //public virtual IEnumerable<TEntity> GetFilteredIncluding(Expression<Func<TEntity, bool>> filter, List<string> objectGraphs)
        //{
        //    if (objectGraphs == null || !objectGraphs.Any()) throw new ArgumentException("ObjectGraph cannot be null or empty!");
        //    var set = GetSet().Include(objectGraphs.FirstOrDefault());
        //    foreach (var s in objectGraphs)
        //        set.Include(s);

        //    return set.Where(filter);
        //}


        public int GetCount()
        {
            return GetSet().Count();
        }

        public int GetCount(Expression<Func<TEntity, bool>> filter)
        {
            return GetSet().Count(filter);
        }

        public virtual void Merge(TEntity persisted, TEntity current)
        {
            _unitOfWork.ApplyCurrentValues(persisted, current);
        }

        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }

        protected IDbSet<TEntity> GetSet()
        {
            return _unitOfWork.CreateSet<TEntity>();
        }

        IEnumerable<TEntity> GetSet(string objectGraph)
        {
            return _unitOfWork.CreateSet<TEntity>(objectGraph);
        }
    }
}