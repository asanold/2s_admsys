using System.ComponentModel.DataAnnotations;

namespace _3TierApp.WEB.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birth { get; set; }
        public int Role { get; set; }
        //public ICollection<DateTime> AccessTimeList { get; set; }
    }
}
