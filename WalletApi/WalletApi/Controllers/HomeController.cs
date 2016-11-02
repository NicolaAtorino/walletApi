using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WalletApi.ServiceLayer;
using WalletApi.Utilities;

namespace WalletApi.Controllers
{
    [RoutePrefix("ping")]
    public class PingController : ApiController
    {
        IAccountService _accountSrv = new AccountService();

        [HttpGet]
        [Route("")]
        public async Task<OperationResult<int>> Ping()
        {
            return _accountSrv.GetAccountId(1);
        }


        
    }
}
