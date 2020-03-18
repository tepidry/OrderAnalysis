using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
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

        // POST api/users
        //[HttpPost]
        //[Route("analyze")]
        //public async Task<ResultValue> Post([Microsoft.AspNetCore.Mvc.FromBody] Order order)
        //{
        //    return new ResultValue() { IsSuccess = true };
        //}

        [HttpPost]
        [Route("test")]
        public async Task<ResultValue> Post([Microsoft.AspNetCore.Mvc.FromBody] object myData)
        {
            MyObject deserializeObject = JsonConvert.DeserializeObject<MyObject>(myData.ToString());
            return new ResultValue() { IsSuccess = true };
        }
    }
}