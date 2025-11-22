using GymManagementDAL.Models;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public ITrainerRepository TrainerRepository { get; }
        public ISessionRepository SessionRepository { get; }
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseModel, new();
        int SaveChanges();
    }
}
