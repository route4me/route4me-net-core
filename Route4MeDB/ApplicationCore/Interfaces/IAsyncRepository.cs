using Microsoft.EntityFrameworkCore;
using Route4MeDB.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Route4MeDB.ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity, IAggregateRoot
    {
        Task<T> GetByIdAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<EntityState> DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}