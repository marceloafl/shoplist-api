using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;
using System.Linq.Expressions;

namespace ShoplistAPI.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetById(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
