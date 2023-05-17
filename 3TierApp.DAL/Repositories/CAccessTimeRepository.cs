using _3TierApp.DAL.EF;
using _3TierApp.DAL.Entities;
using _3TierApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3TierApp.DAL.Repositories
{
    public class CAccessTimeRepository : IRepository<AccessTime>
    {
        private EFCoreMContext db;
        public CAccessTimeRepository(EFCoreMContext context)
        {
            this.db = context;
        }


        public async Task CreateAsync(AccessTime item)
        {
            await db.AccessTime.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            AccessTime access = await db.AccessTime.FindAsync(id);
            if (access != null)
            {
                await db.AccessTime.Where(x => x.Id == id).ExecuteDeleteAsync();
            }
        }

        public async Task<IEnumerable<AccessTime>> FindAsync(Expression<Func<AccessTime, bool>> predicate)
        {
            return await db.AccessTime.Where(predicate).ToListAsync();
            //return (await db.AccessTime.ToListAsync()).Where(predicate);
        }

        public async Task<AccessTime> GetAsync(int id)
        {
            return await db.AccessTime.FindAsync(id);
        }

        public async Task<IEnumerable<AccessTime>> GetAllAsync()
        {
            return await db.AccessTime.ToListAsync();
        }

        public async Task UpdateAsync(AccessTime item)
        {
            await db.AccessTime.Where(x => x.Id == item.Id).ExecuteUpdateAsync(x => x
                                    .SetProperty(u => u.UserId, u => item.UserId)
                                    .SetProperty(u => u.AccessDateTime, u => item.AccessDateTime)
                );
        }
    }
}
