using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task DeleteAsync(T entity);
        Task InsertAsync(T entity);
        Task<IReadOnlyList<T>> SelectAllAsync();
        Task<T> SelectByIdAsync(int id);
        Task UpdateAsync(T entity);
    }
}