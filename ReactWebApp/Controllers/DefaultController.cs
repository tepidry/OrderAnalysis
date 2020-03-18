using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using ReactWebApp.Logic;
using ReactWebApp.Models;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace ReactWebApp.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public System.Web.Mvc.ActionResult Index()
        {
            return View();
        }

        // GET api/users
        [Route("/test")]
        public async Task<ResultValue> Post([Microsoft.AspNetCore.Mvc.FromBody] Order order)
        {
            return new ResultValue() { IsSuccess = true };
        }

    }


    [System.Web.Mvc.RoutePrefix("api/{controller}")]
    public class OrderController : ApiController
    {
        public OrderController()
        {
        }


        [HttpPost]
        [Route("test")]
        public async Task<ResultValue> Post([Microsoft.AspNetCore.Mvc.FromBody] object myData)
        {
            MyObject deserializeObject = JsonConvert.DeserializeObject<MyObject>(myData.ToString());
            return AlgorithmTester.VerifyOrdersAreStocked(deserializeObject.Orders.ToList().AsReadOnly(), deserializeObject.Restocks.ToList().AsReadOnly());
        }
    }
}