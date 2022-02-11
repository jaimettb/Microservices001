using System;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers{
    [Route("api/c/[Controller]")]
    [ApiController]
    public class PlatformsController: ControllerBase{
        public PlatformsController()
        {
            
        }

        public ActionResult TestInboundConnection(){
            Console.WriteLine("--> Inbound POST # Command Service");
            return Ok("Inbound test of form Platforms Controller");
        }
    }
}