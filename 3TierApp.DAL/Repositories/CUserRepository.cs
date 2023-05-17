using _3TierApp.DAL.EF;
using _3TierApp.DAL.Entities;
using _3TierApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _3TierApp.DAL.Repositories
{
    public class CUserRepository : IRepository<User>
    {
        private EFCoreMContext db;
        public CUserRepository(EFCoreMContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(User user)
        {
            await db.Users.AddAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user != null)
            {
                await db.Users.Where(x => x.ID == id).ExecuteDeleteAsync();
                //Console.WriteLine("User Deleted");
                //db.Users.Remove(user);
                //db.Users.Remove(user).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                //db.SaveChanges();
            }
        }

        public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await db.Users.Where(predicate).ToListAsync();
            //return (await db.Users.ToListAsync()).Where(predicate);
        }

        public async Task<User> GetAsync(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await db.Users.ToListAsync();
        }

        public async Task UpdateAsync(User user)
        {
            //db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.Users.Where(x => x.ID == user.ID).ExecuteUpdateAsync(x => x
                                    .SetProperty(u => u.Name, u => user.Name)
                                    .SetProperty(u => u.Birth, u => user.Birth)
                                    .SetProperty(u => u.Role, u => user.Role)
                );
        }
    }
}
