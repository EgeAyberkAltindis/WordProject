
using DAL.Context;
using DAL.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Model.Concretes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly WordContext _context;
        private readonly DbSet<T> _dbSet;   
        public Repository(WordContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async  Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter);
        }

        public async Task<T> CreateAndReturnAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task CreateAsync(T entity)
        {
             await _dbSet.AddAsync(entity);
           await _context.SaveChangesAsync();
        }

        public async  Task CreateRangeAsync(List<T> entities)
        {
            _dbSet.AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllAsync(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        

        public Task<T> GetByIdAsync(int id)
        {
           return _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

       

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedDate = DateTime.Now;
            T originalEntity = await GetByIdAsync(entity.Id);
            _dbSet.Entry(originalEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateRangeAsync(List<T> entities)
        {
            foreach (var item in entities)
            {
                await UpdateAsync(item);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
