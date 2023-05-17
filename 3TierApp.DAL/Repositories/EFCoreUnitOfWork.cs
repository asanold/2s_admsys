using _3TierApp.DAL.EF;
using _3TierApp.DAL.Entities;
using _3TierApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3TierApp.DAL.Repositories
{
    public class EFCoreUnitOfWork : IUnitOfWork
    {
        private EFCoreMContext db;
        private CUserRepository userRepository;
        private CAccessTimeRepository accessTimeRepository;

        public EFCoreUnitOfWork(string connectionString)
        {
            db = new EFCoreMContext(connectionString);
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new CUserRepository(db);
                }
                return userRepository;
            }
        }

        public IRepository<AccessTime> AccessTimes
        {
            get
            {
                if (accessTimeRepository == null)
                {
                    accessTimeRepository = new CAccessTimeRepository(db);
                }
                return accessTimeRepository;
            }
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
