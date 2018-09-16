using CSVUploadToDataTestProject.EntityFramework;
using System.Linq;
using System.Threading.Tasks;

namespace CSVUploadToDataProject.EntityFramework.Repository
{
    public interface IRepostory<TEntity, in TId> where TEntity : class, IHasId<TId>
    {
        IQueryable<TEntity> Query();
        Task SaveAsync(TEntity entity);
        MyDbContext DbContext();
    }
}
