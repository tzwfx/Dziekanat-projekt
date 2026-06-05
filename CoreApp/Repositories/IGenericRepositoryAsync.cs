using CoreApp.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using CoreApp.Common;

namespace CoreApp.Repositories;

public interface IGenericRepositoryAsync<T> where T : BaseEntity
{
    Task<T?> FindByIdAsync(Guid id);
    Task<IEnumerable<T>> FindAllAsync();
    Task<PagedResult<T>> FindPagedAsync(int page, int pageSize);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task RemoveByIdAsync(Guid id);
}