using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApi.DataLayer;
using WalletApi.Repositories.Generic;

namespace WalletApi.Repositories.Account
{
    public class AccountRepository : 
        AbstractRepository<DataLayer.Account>, IAccountRepository
    {
       //all interfaces methods are implemented by base class.
    }
}
