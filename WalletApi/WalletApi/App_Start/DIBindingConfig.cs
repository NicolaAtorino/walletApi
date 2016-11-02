using Ninject.Modules;
using WalletApi.Repositories.Account;
using WalletApi.ServiceLayer;

namespace WalletApi.App_Start
{
    public class DIBindingConfig : NinjectModule
    {
        public override void Load()
        {
            //here we will do all the needed binding for the application 
            //(for now, service and repo)
            Bind<IAccountRepository>().To<AccountRepository>();
            Bind<IAccountService>().To<AccountService>();
        }
    }
}
