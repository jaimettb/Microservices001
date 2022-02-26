using System;
using System.Collections.Generic;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers{
    [Route("api/c/[Controller]")]
    [ApiController]
    public class PlatformsController: ControllerBase{
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms(){
            Console.WriteLine("--> Getting Platforms from CommandService");
            
            var platformItems = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        public ActionResult TestInboundConnection(){
            Console.WriteLine("--> Inbound POST # Command Service");
            return Ok("Inbound test of form Platforms Controller");
        }
    }
}