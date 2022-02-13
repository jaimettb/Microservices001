using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController: ControllerBase{
        private IPlatformRepo _repository;
        private IMapper _mapper;
        private ICommandDataClient _commandDataClient;

        public PlatformsController(
            IPlatformRepo repository,
             IMapper mapper,
             ICommandDataClient commandDataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms(){
            Console.WriteLine("--> Getting Platforms....");

            var platformItem = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }

        [HttpGet("{id}", Name="GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id){
            Console.WriteLine("--> Getting Platform by Id....");

            var platformItem = _repository.GetPlatformById(id);

            if(platformItem != null){
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }
            
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatPlatform(PlatformCreateDto platformCreateDto){
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            try{
                await _commandDataClient.SendPlatformToCommand(platformReadDto).ConfigureAwait(false);
            }
            catch(Exception ex){
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetPlatformById), new {Id = platformReadDto.Id}, platformCreateDto);
        }
    }
}