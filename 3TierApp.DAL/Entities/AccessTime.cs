using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3TierApp.DAL.Entities
{
    public class AccessTime
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime AccessDateTime { get; set; }
    }
}
