namespace WalletApi.Utilities
{
    /// <summary>
    /// The Operation Result of the requested operation. Contains information and the return value as Result.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OperationResult<T> : OperationResult
    {
        /// <summary>
        /// The return value of the operation requested.
        /// </summary>
        public T Result { get; set; }

        public OperationResult()
        {
            Success = true;
        }

        public OperationResult(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
            Result = default(T);
        }

        public OperationResult(T result)
        {
            Success = true;
            Result = result;
        }
    }







}
