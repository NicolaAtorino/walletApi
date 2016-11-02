using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApi.Repositories.Account;
using WalletApi.Utilities;

namespace WalletApi.ServiceLayer
{
    public class AccountService : IAccountService
    {
        IAccountRepository _repo = new AccountRepository();
        public AccountService()
        {

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
            var account = _repo.GetFiltered(x => x.UserId == userId)
                .FirstOrDefault();

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
