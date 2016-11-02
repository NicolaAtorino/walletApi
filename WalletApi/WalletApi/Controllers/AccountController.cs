using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WalletApi.ServiceLayer;
using WalletApi.Utilities;

namespace WalletApi.Controllers
{
    [RoutePrefix("Accounts")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountSrv;
        public AccountController(IAccountService accountSrv)
        {
            _accountSrv = accountSrv;
        }

        [HttpGet]
        [Route("{accountid:int}/balance")]
        public OperationResult<decimal> GetBalance(int accountId)
        {
            if (accountId <= 0)
                SendValidationErrorResult<decimal>(ErrorMessages.NotValidAccountId);


            try
            {
                return _accountSrv.GetBalance(accountId);
            }
            catch (Exception ex)
            {
                return ManageException<decimal>(ex, "GetBalance");
            }

        }

        [HttpPost]
        [Route("{accountid:int}/withdraw")]
        public OperationResult<decimal> WithDraw([FromUri]int accountId, [FromBody]decimal amount)
        {
            if (accountId <= 0)
                SendValidationErrorResult<decimal>(ErrorMessages.NotValidAccountId);

            if (amount <= 0)
                SendValidationErrorResult<decimal>(ErrorMessages.NotValidAmount);
            try
            {
                return _accountSrv.WithDraw(accountId, amount);
            }
            catch (Exception ex)
            {
                return ManageException<decimal>(ex, "Withdraw");
            }

        }

        [HttpPost]
        [Route("{accountid:int}/deposit")]
        public OperationResult<decimal> Deposit([FromUri]int accountId, [FromBody]decimal amount)
        {

            if (accountId <= 0)
                SendValidationErrorResult<decimal>(ErrorMessages.NotValidAccountId);

            if (amount <= 0)
                SendValidationErrorResult<decimal>(ErrorMessages.NotValidAmount);

            try
            {
                return _accountSrv.Deposit(accountId, amount);
            }
            catch (Exception ex)
            {
                return ManageException<decimal>(ex, "Deposit");
            }

        }


        /// <summary>
        /// This Method will disable an account. 
        /// In a real-world app should be used only via a specific authorization
        /// </summary>
        [HttpPost]
        [Route("{accountid:int}/management/disable")]
        public OperationResult DisableAccount([FromUri]int accountId)
        {
            if (accountId <= 0)
                SendValidationErrorResult<decimal>(ErrorMessages.NotValidAccountId);

            try
            {
                return _accountSrv.DisableAccount(accountId);
            }
            catch (Exception ex)
            {
                return ManageException(ex, "Deposit");
            }
        }
    }
}
