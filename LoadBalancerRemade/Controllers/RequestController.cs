using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LoadBalancerRemade.Models;
namespace LoadBalancerRemade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        RequestAndResponseHandler RnRHandler = new RequestAndResponseHandler();
        // GET: api/Request
        [Route("{num1}/{num2}")]
        public Task<long> get(long num1, long num2)
        {

            var Response = RnRHandler.sendRequestToService(num1, num2);

            return Response;
        }
    }
}
