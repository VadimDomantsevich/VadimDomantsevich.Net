using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.EntityFramework
{
    public class GeneralRepository<T> : IRepository<T>
        where T : class
    {
        private readonly DBContext _dBContext;

        public GeneralRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task Create(T item)
        {
            await _dBContext.Set<T>().AddAsync(item);
            await _dBContext.SaveChangesAsync();
        }

        public async Task Delete(T item)
        {
            _dBContext.Set<T>().Remove(item);
            await _dBContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll()
        {
            var items = await _dBContext.Set<T>().AsNoTracking().ToListAsync();
            return items;
        }

        public async Task<T> GetById(int id)
        {
            var item = await _dBContext.Set<T>().FindAsync(id);

            _dBContext.Entry(item).State = EntityState.Detached;

            return item;
        }

        public async Task Update(T item)
        {
            _dBContext.Entry(item).State = EntityState.Modified;

            await _dBContext.SaveChangesAsync();
        }
    }
}
