using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Models;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel, new()
    {
        private readonly GymContext _context;

        public GenericRepository(GymContext context)
        {
            _context = context;
        }
        public void Add(TEntity entity) => _context.Add(entity);
        

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? func = null)
        {
            if(func == null)
                return _context.Set<TEntity>().AsNoTracking().ToList();
            else
                return _context.Set<TEntity>().AsNoTracking().Where(func).ToList();
        }

        public TEntity? GetById(int id) => _context.Set<TEntity>().Find(id);
        
        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);
        
    }
}
