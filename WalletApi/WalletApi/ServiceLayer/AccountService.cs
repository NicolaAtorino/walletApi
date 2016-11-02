using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApi.DataLayer;
using WalletApi.Repositories.Account;
using WalletApi.Utilities;

namespace WalletApi.ServiceLayer
{
    public class AccountService : IAccountService
    {
        private static ILogger _logger;

        private readonly IAccountRepository _repo;
        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public OperationResult<decimal> Deposit(int accountId, decimal amount)
        {
            try
            {
                Account account = null;
                account = _repo.Get(accountId);

                if (account == null)
                    return new OperationResult<decimal>(ErrorMessages.AccountNotFound);

                if (!account.Enabled)
                    return new OperationResult<decimal>(ErrorMessages.AccountIsDisabled);

                decimal newBalance;

                account.Balance += amount;
                _repo.Update(account);

                account = _repo.Get(accountId);
                newBalance = account.Balance;

                return new OperationResult<decimal>(newBalance);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Deposit Error. AccountId : {accountId}");
                return new OperationResult<decimal>(ErrorMessages.GenericError);
            }
        }

        public OperationResult DisableAccount(int accountId)
        {
            try
            {
                Account account = null;
                account = _repo.Get(accountId);

                if (account == null)
                    return new OperationResult(ErrorMessages.AccountNotFound);

                if (!account.Enabled)
                    return new OperationResult(ErrorMessages.AccountIsDisabled);

                if (account.Balance > 0)
                    return new OperationResult(ErrorMessages.AccountHasMoney);

                account.Enabled = false;
                _repo.Update(account);

                return new OperationResult();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"DisableAccount Error. AccountId : {accountId}");
                return new OperationResult(ErrorMessages.GenericError);
            }
        }

        public OperationResult<int> GetAccountId(int userId)
        {
            Account account = null;
            try
            {
                account = _repo
                    .GetFiltered(x => x.UserId == userId)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"GetAccountId error. UserId : {userId}");
                return new OperationResult<int>(ErrorMessages.GenericError);
            }


            if (account == null)
            {
                return new OperationResult<int>(ErrorMessages.AccountNotFound);
            }
            return new OperationResult<int>(account.Id);
        }

        public OperationResult<decimal> GetBalance(int accountId)
        {
            Account account = null;
            try
            {
                account = _repo.Get(accountId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"GetBalance error. AccountId : {accountId}");
                return new OperationResult<decimal>(ErrorMessages.GenericError);
            }
            if (account == null)
            {
                return new OperationResult<decimal>(ErrorMessages.AccountNotFound);
            }
            return new OperationResult<decimal>(account.Balance);
        }

        public OperationResult<decimal> WithDraw(int accountId, decimal amount)
        {
            try
            {
                Account account = null;
                account = _repo.Get(accountId);

                if (account == null)
                    return new OperationResult<decimal>(ErrorMessages.AccountNotFound);

                if (!account.Enabled)
                    return new OperationResult<decimal>(ErrorMessages.AccountIsDisabled);

                if (account.Balance < amount)
                    return new OperationResult<decimal>(ErrorMessages.NotEnoughMoneyOnAccount);

                decimal newBalance;

                account.Balance -= amount;
                _repo.Update(account);

                account = _repo.Get(accountId);
                newBalance = account.Balance;

                return new OperationResult<decimal>(newBalance);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Withdraw Error. AccountId : {accountId}");
                return new OperationResult<decimal>(ErrorMessages.GenericError);
            }
        }
    }
}
