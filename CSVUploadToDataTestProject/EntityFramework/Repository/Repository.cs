using CSVUploadToDataTestProject.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVUploadToDataProject.EntityFramework.Repository
{

    public class Repository<TEntity, TId> : IRepostory<TEntity, TId> where TEntity : class, IHasId<TId>
    {
        private readonly MyDbContext _dbContext;

        private readonly DbSet<TEntity> _set;

        public Repository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
            _set = _dbContext.Set<TEntity>();
        }

        public async Task SaveAsync(TEntity entity)
        {
            var isModified = entity.Id.Equals(default(TId)) ?  EntityState.Added : EntityState.Modified;                         

            if(isModified == EntityState.Modified)
            {
                _dbContext.Update(entity);
            }
            else
            {
                await _set.AddAsync(entity);
            }            

            await _dbContext.SaveChangesAsync(true);
        }

        public async Task Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>().AsQueryable();         
        }

        public MyDbContext DbContext()
        {
            return _dbContext;
        }
    }
}
