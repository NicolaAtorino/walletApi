using WalletApi.Utilities;

namespace WalletApi.ServiceLayer
{
    public interface IAccountService
    {
        OperationResult<int> GetAccountId(int userId);
        OperationResult<decimal> GetBalance(int accountId);
        OperationResult<decimal> WithDraw(int accountId,decimal amount);
        OperationResult<decimal> Deposit(int accountId, decimal amount);
        OperationResult DisableAccount(int accountId);
    }
}
