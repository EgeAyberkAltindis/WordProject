using Model.Concretes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Abstract
{
    public interface IRepository<T> where T : BaseEntity 
    {

        Task<T> CreateAndReturnAsync(T entity);
        IQueryable<T> GetAll();
         Task<T> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        Task CreateRangeAsync(List<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(List<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteAllAsync(List<T> entities);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        Task SaveChangesAsync();



    }
}
