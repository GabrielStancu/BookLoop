using Core.Helpers;
using Core.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        protected readonly BookContext Context;

        public GenericRepository(BookContext context)
        {
            Context = context;
        }

        public async Task<T> SelectByIdAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> SelectAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task InsertAsync(T entity)
        {
            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            Context.Set<T>().Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
