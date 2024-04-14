using Domain;

namespace Application.Interfaces.Repositorys;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAll();

    Task<TEntity> GetByIdAsync(Guid id);

    Task AddAsync(TEntity entity);

    Task Update(TEntity entity);

    Task Delete(TEntity entity);

    Task SaveChangesAsync();
}
