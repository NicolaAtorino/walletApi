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
    /// <summary>
    /// Handles all the operations possible at account level.
    /// </summary>
    [RoutePrefix("Accounts")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountSrv;
        public AccountController(IAccountService accountSrv)
        {
            _accountSrv = accountSrv;
        }

        /// <summary>
        /// Check the balance in an account by providing the ID
        /// </summary>
        /// <param name="accountId">The Id of the account.</param>
        /// <returns>The result of the operation with the balance as a decimal, 
        /// or an error message in case of failure.</returns>
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

        /// <summary>
        /// Withdraw a specific amount of money from an account.
        /// </summary>
        /// <param name="accountId">The Id of the account</param>
        /// <param name="amount">The aount to withdraw. As to be > 0.</param>
        /// <returns>The result of the operation with the updated balance as a decimal, 
        /// or an error message in case of failure.</returns>
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

        /// <summary>
        /// Deposit a specific amount on money into an account.
        /// </summary>
        /// <param name="accountId">The id of the account.</param>
        /// <param name="amount">The amount to deposit. Has to be > 0</param>
        /// <returns>The result of the operation with the updated balance as a decimal, 
        /// or an error message in case of failure.</returns>
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
        /// <param name="accountId">The id of the account.</param>
        /// <returns>The result of the operation, with an error message in case of failure.</returns>
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
