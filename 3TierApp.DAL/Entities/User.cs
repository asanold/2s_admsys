using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3TierApp.DAL.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public int Role { get; set; }
        //public ICollection<DateTime> AccessTimeList { get; set; }


    }

    public enum Role
    {
        Admin = 1,
        DefaulUser = 2
    }
}
