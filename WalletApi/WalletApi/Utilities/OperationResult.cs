using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Utilities
{
    /// <summary>
    /// The base operationResult object. Contains information about the operation requested.
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// The result of the operation.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// A specific error message in case of failure.
        /// </summary>
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
