using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Models;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, Object> _Repositories = new();
        private readonly GymContext _context;

        public UnitOfWork(GymContext context, ITrainerRepository trainerRepository, ISessionRepository sessionRepository)
        {
            this._context = context;
            TrainerRepository = trainerRepository;
            SessionRepository = sessionRepository;
        }

        public ITrainerRepository TrainerRepository { get; }

        public ISessionRepository SessionRepository {  get; }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseModel, new()
        {
            if (_Repositories.TryGetValue(typeof(TEntity), out var repo))
                return (IGenericRepository<TEntity>)repo;
            var newRepo = new GenericRepository<TEntity>(_context);
            _Repositories.Add(typeof(TEntity), newRepo);
            return newRepo;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
