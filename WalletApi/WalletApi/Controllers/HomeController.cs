using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WalletApi.ServiceLayer;
using WalletApi.Utilities;

namespace WalletApi.Controllers
{
    [RoutePrefix("ping")]
    public class PingController : ApiController
    {
        [HttpGet]
        [Route("")]
        public string Ping()
        {
            return "Pong";
        }


        
    }
}
