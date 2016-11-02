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
        private readonly IAccountRepository _repo;
        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }
        
        public OperationResult<decimal> Deposit(decimal amount)
        {
            throw new NotImplementedException();
        }

        public OperationResult DisableAccount(int accountId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public OperationResult<decimal> WithDraw(decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
