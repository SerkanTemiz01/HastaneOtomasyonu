using Hastane.Core.DataAccess.Abstract;
using Hastane.Core.Entities.Abstract;
using Hastane.Core.Enums;
using Hastane.DataAccess.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane.DataAccess.EntityFramework.Concrete
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class, IBaseEntity
    {
        private readonly HastaneDbContext _hastaneDbContext;
        protected DbSet<T> _dbSet;
        public BaseRepo(HastaneDbContext hastaneDbContext)
        {
            _hastaneDbContext = hastaneDbContext;
            _dbSet = _hastaneDbContext.Set<T>();
        }

        public async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await Save() > 0;
        }

        public async Task<bool> AddRange(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return await Save() > 0;
        }

        public async Task<bool> Delete(T entity)
        {
            entity.Status = Status.Passive;
            return await Save()>0;
        }

        public async Task<List<T>> GetAll() => await _dbSet.Where(x=>x.Status== Status.Active).ToListAsync();


        public async Task<T> GetById(Guid id) => await _dbSet.FindAsync(id);
       

        public async Task<int> Save()
        {
            return await _hastaneDbContext.SaveChangesAsync();
        }

        public async Task<bool> Update(T entity)
        {
           _hastaneDbContext.Entry<T>(entity).State = EntityState.Modified;
            return await Save() > 0;
        }

        
    }
}
