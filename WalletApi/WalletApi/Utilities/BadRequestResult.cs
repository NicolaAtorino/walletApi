using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WalletApi.Utilities
{
    public class BadRequestResult<T> : IHttpActionResult
    {
        private string message;

        public BadRequestResult(string message)
        {
            this.message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            var result = new OperationResult<T>(message); 
            response.Content = new StringContent(JsonConvert.SerializeObject(result));
            return Task.FromResult(response);
        }
    }
}
