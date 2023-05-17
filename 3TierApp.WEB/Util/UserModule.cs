using Ninject.Modules;
using _3TierApp.BLL.Services;
using _3TierApp.BLL.Interfaces;
namespace _3TierApp.WEB.Util
{
    public class UserModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();
        }
    }
}
