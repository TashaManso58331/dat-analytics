using Dat.Access.Clients;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Dat.Access.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IDatService datClient;

        public AccessController(IDatService datClient)
        {
            this.datClient = datClient;
        }

        [HttpGet]
        public String Get()
        {
            return datClient.GetAllTokens();
        }
    }
}
