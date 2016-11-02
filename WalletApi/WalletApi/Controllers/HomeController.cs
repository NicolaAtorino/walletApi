using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WalletApi.Controllers
{
    [RoutePrefix("ping")]
    public class PingController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<string> Ping()
        {
            return await Task.FromResult("Pong");
        }
    }
}
