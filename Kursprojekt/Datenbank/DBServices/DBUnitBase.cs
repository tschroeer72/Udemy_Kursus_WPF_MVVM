using System.Linq.Expressions;
using Kursprojekt.Datenbank.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Kursprojekt.Datenbank.DBServices;

public class DBUnitBase<ModelName> where ModelName : class
{
    private readonly ApplicationDBContext _DBContext;
    private readonly DbSet<ModelName> _DbSet;

    public DBUnitBase()
    {
        _DBContext = new();
        _DbSet = _DBContext.Set<ModelName>();
    }

    public async Task<DBResponse> AddAsync(ModelName model)
    {
        DBResponse objResponse = new();
        try
        {
            var erg = _DbSet.AddAsync(model);
            await _DBContext.SaveChangesAsync();
            objResponse.Success = true;
            return objResponse;
        }
        catch (Exception ex)
        {
            objResponse.Message = ex.Message;
            return objResponse;
        }
    }
    
    public async Task<DBResponse> DeleteByIDAsync(int ID)
    {
        DBResponse objResponse = new();
        try
        {
            if (ID > 0)
            {
                ModelName? modelName = await GetByIDAsync(ID);
                if (modelName == null)
                {
                    objResponse.Message = $"Object mit der ID {ID} wurde nicht gefunden.";
                    return objResponse;
                }
                _DbSet.Remove(modelName);
                 await _DBContext.SaveChangesAsync();
                 objResponse.Success = true;
                 return objResponse;
            }

            objResponse.Message = $"Object mit der ID {ID} wurde nicht gefunden.";
            return objResponse;
        }
        catch (Exception ex)
        {
            objResponse.Message = ex.Message;
            return objResponse;
        }
    }

    public async Task<DBResponse> UpdateAsync(ModelName model)
    {
        DBResponse objResponse = new();
        try
        {
            _DbSet.Update(model);
            await _DBContext.SaveChangesAsync();

            objResponse.Success = true;
            return objResponse;
        }
        catch (Exception ex)
        {
            objResponse.Message = ex.Message;
            return objResponse;
        }
    }

    public async Task<ModelName?> GetByIDAsync(int ID)
    {
        try
        {
            if (ID > 0)
            {
                return await _DbSet.FindAsync(ID);
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
    
    public async Task<ModelName?> GetFirstOrDefaultAsync(Expression<Func<ModelName, bool>>? filter = null, string? includeProperties = null)
    {
        try
        {
            IQueryable<ModelName> query = _DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                var includeModels = includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries);
                foreach (var includeModel in includeModels)
                {
                    query = query.Include(includeModel);
                }
            }

            return await query.FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            return null;
        }

    }
    
    public async Task<IEnumerable<ModelName>?> GetAllAsync(Expression<Func<ModelName, bool>>? filter = null, string? includeProperties = null)
    {
        try
        {
            IQueryable<ModelName> query = _DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                var includeModels = includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries);
                foreach (var includeModel in includeModels)
                {
                    query = query.Include(includeModel);
                }
            }

            return await query.ToListAsync();
        }
        catch (Exception)
        {
            return null;
        }
    }
}