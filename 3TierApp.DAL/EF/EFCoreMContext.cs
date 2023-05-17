using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3TierApp.DAL.Entities;
//using System.Data.Entity;
using Microsoft.CSharp;
using Microsoft.EntityFrameworkCore;

namespace _3TierApp.DAL.EF
{
    public class EFCoreMContext : Microsoft.EntityFrameworkCore.DbContext
    {

        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<AccessTime> AccessTime { get; set; }
        private string _source;

        public EFCoreMContext(string source)
        {
            _source = source;
            Database.EnsureCreated();
        }
        //protected override void ConfigureConvensions()
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string source = "Data Source=" + _source;
            optionsBuilder.UseSqlite(source);
        }
    }

    //public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    //{
    //    /// <summary>
    //    /// Creates a new instance of this converter.
    //    /// </summary>
    //    public DateOnlyConverter() : base(
    //            d => d.ToDateTime(TimeOnly.MinValue),
    //            d => DateOnly.FromDateTime(d))
    //    { }
    //}

    public static class CUserAdminisrationDBInitializer
    {
        public static void Run(string source)
        {
            using (EFCoreMContext db = new EFCoreMContext(source))
            {
                db.Users.Add(new User { Name = "Alex Bebrz", Birth = new DateTime(1999, 9, 11), Role = 0 });
                db.Users.Add(new User { Name = "Oleg Bobrz", Birth = new DateTime(2001, 9, 11), Role = 1 });
                db.Users.Add(new User { Name = "Bernz Schkripper", Birth = new DateTime(1958, 11, 16), Role = 0 });
                db.Users.Add(new User { Name = "Schkrapz Schrooper", Birth = new DateTime(1999, 5, 2), Role = 0 });
                db.Users.Add(new User { Name = "Guys Sorry", Birth = new DateTime(1992, 12, 22), Role = 0 });
                db.Users.Add(new User { Name = "Oleg Dedeutsch", Birth = new DateTime(1999, 9, 11), Role = 0 });
                db.Users.Add(new User { Name = "Alex Dedeutsch", Birth = new DateTime(2001, 9, 11), Role = 0 });
                db.Users.Add(new User { Name = "Schprehende Deutsch", Birth = new DateTime(1945, 5, 30), Role = 0 });
                db.SaveChanges();
                db.Dispose();
            }
        }
    }
}
