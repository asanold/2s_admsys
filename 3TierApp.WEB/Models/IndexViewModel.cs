using _3TierApp.DAL.Entities;

namespace _3TierApp.WEB.Models
{
    public class IndexViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
