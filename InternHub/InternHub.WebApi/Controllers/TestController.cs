using InternHub.Model.Common;
using InternHub.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InternHub.WebApi.Controllers
{
    public class TestController : ApiController
    {
        public IStateService State { get; }

        public TestController(IStateService state)
        {
            State = state;
        }

        public HttpResponseMessage Post()
        {
            //State.Add();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Authorize]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "test");
        }
    }
}
