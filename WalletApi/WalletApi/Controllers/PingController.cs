using System.Web.Http;

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
