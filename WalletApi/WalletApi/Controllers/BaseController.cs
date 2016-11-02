#define DEBUG
using NLog;
using System;
using System.Web.Http;
using WalletApi.Utilities;

namespace WalletApi.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected static ILogger _logger = LogManager.GetCurrentClassLogger();

        protected OperationResult ManageException(Exception ex, string action, params object[] actionParams)
        {
            LogException(ex, action, actionParams);
            return new OperationResult(ErrorMessages.GenericError);
        }

        protected OperationResult<T> ManageException<T>(Exception ex, string action, params object[] actionParams)
        {
            LogException(ex, action, actionParams);
            return new OperationResult<T>(ErrorMessages.GenericError);
        }

        private void LogException(Exception ex, string action, object[] actionParams)
        {
            _logger.Error(ex, $@"Error during Execution of controller {this.GetType()} 
                                 - action {action} with parameters {string.Join(",", actionParams)}");
        }
    }
}
