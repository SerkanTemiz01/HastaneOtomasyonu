using Hastane.Core.DataAccess.Abstract;
using Hastane.Core.Entities.Abstract;
using Hastane.Core.Enums;
using Hastane.DataAccess.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            entity.DeletedDate= DateTime.Now;
            return await Save()>0;
        }

        public async Task<List<T>> GetAll() => await _dbSet.Where(x=>x.Status==Status.Active||x.Status==Status.Modified).ToListAsync();


        public async Task<T> GetById(Guid id) => await _dbSet.FindAsync(id);
       

        public async Task<int> Save()
        {
            return await _hastaneDbContext.SaveChangesAsync();
        }

        public async Task<bool> Update(T entity)
        {
           _hastaneDbContext.Entry<T>(entity).State = EntityState.Modified;
            entity.ModifiedDate= DateTime.Now;
            entity.Status= Status.Modified;
            return await Save() > 0;
        }
		public async Task<T> GetDefault(Expression<Func<T, bool>> expression)
		{
			return await _dbSet.FirstOrDefaultAsync(expression);
		}



		public async Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression)
		{
			return await _dbSet.Where(expression).ToListAsync();
		}



		public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
		{
			IQueryable<T> query = _dbSet;



			if (where != null)
			{
				query = query.Where(where);
			}



			if (include != null)
			{
				query = include(query);
			}



			if (orderBy != null)
			{
				return await orderBy(query).Select(select).FirstOrDefaultAsync();
			}
			else
			{
				return await query.Select(select).FirstOrDefaultAsync();
			}
		}



		public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
		{
			IQueryable<T> query = _dbSet;



			if (where != null)
			{
				query = query.Where(where);
			}



			if (include != null)
			{
				query = include(query);
			}



			if (orderBy != null)
			{
				return await orderBy(query).Select(select).ToListAsync();
			}
			else
			{
				return await query.Select(select).ToListAsync();
			}
		}

	}
}
