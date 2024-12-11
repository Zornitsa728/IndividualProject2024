using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Repository.Interfaces;

namespace RecipeApp.Data.Repository
{
    public class BaseRepository<TType, TId> : IRepository<TType, TId>
         where TType : class
    {
        private readonly RecipeDbContext dbContext;
        private readonly DbSet<TType> dbSet;
        public BaseRepository(RecipeDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Set<TType>();
        }
        public TType GetById(TId id)
        {
            if (id is object[] compositeKeyValues)
            {
                var entity = dbSet.Find(compositeKeyValues);
                return entity!;
            }

            return dbSet.Find(id)!;
        }
        public async Task<TType> GetByIdAsync(TId id)
        {
            if (id is object[] compositeKeyValues)
            {
                var entity = await dbSet.FindAsync(compositeKeyValues);
                return entity!;
            }

            return await dbSet.FindAsync(id);
        }
        public IEnumerable<TType> GetAll()
        {
            return dbSet.ToArray();
        }
        public async Task<IEnumerable<TType>> GetAllAsync()
        {
            return await dbSet.ToArrayAsync();
        }
        public IQueryable<TType> GetAllAttached()
        {
            return dbSet.AsQueryable();
        }
        public void Add(TType item)
        {
            dbSet.Add(item);
            dbContext.SaveChanges();
        }
        public async Task AddAsync(TType item)
        {
            await dbSet.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }
        public bool Delete(TId id)
        {
            TType? entity = id is object[] compositeKeyValues
               ? dbSet.Find(compositeKeyValues)
               : dbSet.Find(id);

            if (entity == null)
            {
                return false;
            }

            dbSet.Remove(entity);
            dbContext.SaveChanges();
            return true;
        }
        public async Task<bool> DeleteAsync(TId id)
        { //the order of ckv is important 
            TType? entity = id is object[] compositeKeyValues
              ? await dbSet.FindAsync(compositeKeyValues)
              : await dbSet.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }
        public bool Update(TType item)
        {
            try
            {
                dbSet.Attach(item);
                dbContext.Entry(item).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<bool> UpdateAsync(TType item)
        {
            try
            {
                dbSet.Attach(item);
                dbContext.Entry(item).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
