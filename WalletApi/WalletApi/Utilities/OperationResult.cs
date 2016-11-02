using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Utilities
{
    public class OperationResult
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public OperationResult()
        {
            Success = true;
        }

        public OperationResult(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
        }
    }







}
