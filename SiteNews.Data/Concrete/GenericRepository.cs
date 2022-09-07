using Microsoft.EntityFrameworkCore;
using SiteNews.Data.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteNews.Data.Concrete
{
    public class GenericRepository<Tentity> : IRepository<Tentity> where Tentity : class
    {
        protected readonly DbContext context;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Create(Tentity entity)
        {
            context.Set<Tentity>().Add(entity);
        }

        public async Task CreateAsync(Tentity entity)
        {
            await context.Set<Tentity>().AddAsync(entity);
        }

        public void Delete(Tentity entity)
        {
            context.Set<Tentity>().Remove(entity);
        }

        public async Task<List<Tentity>> GetAll()
        {
            return await context.Set<Tentity>().AsNoTracking().ToListAsync();
        }

        public async Task<Tentity> GetById(int id)
        {
            return await context.Set<Tentity>().FindAsync(id);
        }

        public void Update(Tentity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
