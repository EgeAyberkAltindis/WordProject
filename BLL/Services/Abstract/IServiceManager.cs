using Model.Concretes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface IServiceManager<T> where T : BaseEntity
    {
     
        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int id);


        //Modified Commands

        Task CreateAsync(T entity);

        Task CreateRangeAsync(List<T> entities);

        Task UpdateAsync(T entity);

        Task UpdateRangeAsync(List<T> entities);

        Task DeleteAsync(T entity);

        Task DeleteAllAsync(List<T> entities);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        
    }
}
