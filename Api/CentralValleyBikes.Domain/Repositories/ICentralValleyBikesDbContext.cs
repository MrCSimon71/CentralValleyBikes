using Microsoft.EntityFrameworkCore;

namespace CentralValleyBikes.Domain.Repositories
{
    public interface ICentralValleyBikesDbContext<TEntity> where TEntity : class
    {
        DbSet<TEntity> SetDbSet(TEntity entity);
    }
}
