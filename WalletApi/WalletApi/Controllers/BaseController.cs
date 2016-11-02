#define DEBUG
using Newtonsoft.Json;
using NLog;
using System;
using System.Net;
using System.Net.Http;
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

        protected void SendValidationErrorResult<T>(string errorMessage)
        {
            var result = new OperationResult<T>(errorMessage);
            CreateAndThrowResponseException(result);
        }

        protected void SendValidationErrorResult(string errorMessage)
        {
            var result = new OperationResult(errorMessage);
            CreateAndThrowResponseException(result);
        }

        private void CreateAndThrowResponseException(object result)
        {
            var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(JsonConvert.SerializeObject(result)),
                ReasonPhrase = ErrorMessages.NotValidAccountId
            };
            throw new HttpResponseException(resp);
        }

        private void LogException(Exception ex, string action, object[] actionParams)
        {
            _logger.Error(ex, $@"Error during Execution of controller {this.GetType()} 
                                 - action {action} with parameters {string.Join(",", actionParams)}");
        }
    }
}
