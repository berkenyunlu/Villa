using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using System.Linq.Expressions;
using Villa.DataAccess.Abstract;
using Villa.DataAccess.Context;

namespace Villa.DataAccess.Repositories;

public class GenericRepository<T> : IGenericDal<T> where T : class
{
    private readonly VillaContext _context;

    public GenericRepository(VillaContext context)
    {
        _context = context;
    }
    private DbSet<T> Table { get => _context.Set<T>(); }
    public async Task<int> CountAsync()
    {
        return await Table.CountAsync();
    }

    public async Task CreateAsync(T entity)
    {
        await Table.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ObjectId id)
    {
        var value = await GetByIdAsync(id);
        _context.Remove(value);
        await _context.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
    {
        IQueryable<T> query = Table;
        if(predicate != null)
            query=query.Where(predicate);

        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(ObjectId id)
    {
        return await Table.FindAsync(id);
    }


    public async Task UpdateAsync(T entity)
    {
        Table.Update(entity);
        await _context.SaveChangesAsync();
    }
}
