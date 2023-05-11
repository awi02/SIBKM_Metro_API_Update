using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Repositories
{
    public interface IGeneralRepos<TEntity,TKey>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetByKEY(TKey key);
        int Insert(TEntity entity);
        int Update(TEntity entity);
        int Delete(TKey key);
    }
}
