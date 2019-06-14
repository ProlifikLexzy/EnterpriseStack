using MyApp.Shared.EF;
using MyApp.Shared.EF.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Shared.EF.Services
{
    public class Service<TEntity> : IService<TEntity>, IDataErrorInfo where TEntity : BaseEntity
    {
        public IUnitOfWork UnitOfWork { get; private set; }
        private readonly IRepository<TEntity> _repository;
        private bool _disposed;
        protected Dictionary<String, String> _errors = new Dictionary<String, String>();
        protected List<ValidationResult> results = new List<ValidationResult>();

        public Service(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _repository = UnitOfWork.Repository<TEntity>();
        }

        public IQueryable<TEntity> SqlRawQuery(string sql, params object[] parameters)
        {
            return _repository.SqlRawQuery(sql, parameters);
        }

        protected bool IsValid<T>(T entity)
        {
            return Validator.TryValidateObject(entity, new ValidationContext(entity, null, null),
              results, false);
        }

        public TEntity SingleOrDefault(Func<TEntity, bool> predicate)
        {
            return _repository.GetAll().SingleOrDefault(predicate);
        }

        public TEntity SingleOrDefault()
        {
            return _repository.GetAll().SingleOrDefault();
        }

        public TEntity FirstOrDefault(Func<TEntity, bool> predicate)
        {
            return _repository.GetAll().FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> All { get { return _repository.Query(); } }

        public TEntity FirstOrDefault()
        {
            return _repository.GetAll().FirstOrDefault();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<TEntity> GetAll(int pageIndex, int pageSize)
        {
            return _repository.GetAll(pageIndex, pageSize);
        }

        public IQueryable<TEntity> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, Guid>> keySelector, OrderBy orderBy = OrderBy.Ascending)
        {
            return _repository.GetAll(pageIndex, pageSize, keySelector, orderBy);
        }

        public IQueryable<TEntity> GetAllString(int pageIndex, int pageSize, Expression<Func<TEntity, Guid>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params string[] includeProperties)
        {

            return _repository.GetAllString(pageIndex, pageSize, keySelector, predicate, orderBy, includeProperties);
        }

        public IQueryable<TEntity> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, Guid>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.GetAll(pageIndex, pageSize, keySelector, predicate, orderBy, includeProperties);
        }

        public TEntity GetById(Guid id)
        {
            return _repository.GetSingle(id);
        }

        public void Add(TEntity entity)
        {
            _repository.Insert(entity);
            UnitOfWork.SaveChanges();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _repository.InsertRange(entities);
            UnitOfWork.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
            UnitOfWork.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _repository.Delete(entity);
            UnitOfWork.SaveChanges();
        }


        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PaginatedList<TEntity>> GetAllAsync(int pageIndex, int pageSize)
        {
            return await _repository.GetAllAsync(pageIndex, pageSize);
        }

        public async Task<PaginatedList<TEntity>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<TEntity, Guid>> keySelector, OrderBy orderBy = OrderBy.Ascending)
        {
            return await _repository.GetAllAsync(pageIndex, pageSize, keySelector, orderBy);
        }

        public async Task<PaginatedList<TEntity>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<TEntity, Guid>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await _repository.GetAllAsync(pageIndex, pageSize, keySelector, predicate, orderBy, includeProperties);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Int32> AddAsync(TEntity entity)
        {
            _repository.Insert(entity);
            return await UnitOfWork.SaveChangesAsync();
        }

        public async Task<Int32> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            _repository.InsertRange(entities);
            return await UnitOfWork.SaveChangesAsync();
        }

        public async Task<Int32> UpdateAsync(TEntity entity)
        {
            _repository.Update(entity);
            return await UnitOfWork.SaveChangesAsync();
        }

        public async Task<Int32> DeleteAsync(TEntity entity)
        {
            _repository.Delete(entity);
            return await UnitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                UnitOfWork.Dispose();
            }
            _disposed = true;
        }

        public string Error
        {
            get
            {
                if (_errors.Count > 0)
                {
                    return _errors.FirstOrDefault().Value;
                }
                return String.Empty;
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (_errors.ContainsKey(columnName))
                {
                    return _errors[columnName];
                }

                return String.Empty;
            }
        }

        public Boolean HasError
        {
            get
            {
                if (_errors.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
