using NLog;
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
    [RoutePrefix("Users")]
    public class UserController : BaseController
    {
        private readonly IAccountService _accountSrv;
        public UserController(IAccountService accountSrv)
        {
            _accountSrv = accountSrv;
        }

        [HttpGet]
        [Route("{userid:int}/accountId")]
        public OperationResult<int> GetAccount(int userid)
        {
            try
            {
                return _accountSrv.GetAccountId(userid);
            }
            catch (Exception ex)
            {
                return ManageException<int>(ex, "GetAccount");
            }

        }
    }
}
