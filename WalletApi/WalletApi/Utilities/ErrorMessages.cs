using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Utilities
{
    public static class ErrorMessages
    {
        public static string AccountNotFound = "Account not found";

        public static string GenericError = "An error has occurred";

        public static string AccountIsDisabled = "Account is disabled by admin.";

        public static string NotEnoughMoneyOnAccount = "Not enough money on the account";

        public static string AccountHasMoney = "Account still has money hence it cannot be closed";

        public static string NotValidUserId = "The UserId is not valid";

        public static string NotValidAccountId = "The AccountID is not valid";

        public static string NotValidAmount = "The amount is not valid";
    }
}
