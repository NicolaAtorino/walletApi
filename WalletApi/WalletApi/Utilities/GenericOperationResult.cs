namespace WalletApi.Utilities
{
    public class OperationResult<T> : OperationResult
    {
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
