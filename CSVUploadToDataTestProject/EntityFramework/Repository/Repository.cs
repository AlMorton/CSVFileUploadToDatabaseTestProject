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
            _dbContext.Entry(entity).State = entity.Id.Equals(default(TId)) ?  EntityState.Added : EntityState.Modified;                         

            if(_dbContext.Entry(entity).State == EntityState.Modified)
            {
                _set.Update(entity);
            }
            else
            {
                await _set.AddAsync(entity);
            }            

            await _dbContext.SaveChangesAsync(true);
        }

        public async Task<int> SaveManyAsync(List<TEntity> entities)
        {
            entities.ForEach(e =>
            {
                _dbContext.Entry(e).State = e.Id.Equals(default(TId)) ? EntityState.Added : EntityState.Modified;
            });

            var updateList = entities.Where(e => _dbContext.Entry(e).State == EntityState.Modified).AsEnumerable();

            entities.RemoveAll(e => _dbContext.Entry(e).State == EntityState.Modified);

            _set.UpdateRange(updateList);

            await _set.AddRangeAsync(entities);

            return await _dbContext.SaveChangesAsync();            
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
