using BLL.Services.Abstract;
using DAL.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Model.Concretes;
using Model.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concretes
{
    public class ServiceManager<T> : IServiceManager<T> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;
        public ServiceManager(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await _repository.AnyAsync(filter);
        }

        public Task CreateAsync(T entity)
        {
           return _repository.CreateAsync(entity);
        }

        public Task CreateRangeAsync(List<T> entities)
        {
            return _repository.CreateRangeAsync(entities);
        }

        public Task DeleteAllAsync(List<T> entities)
        {
           return _repository.DeleteAllAsync(entities);
        }

        public Task DeleteAsync(T entity)
        {
            return _repository.DeleteAsync(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<T> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            return _repository.UpdateAsync(entity);
        }

        public Task UpdateRangeAsync(List<T> entities)
        {
            return _repository.UpdateRangeAsync(entities);
        }
    }
}
