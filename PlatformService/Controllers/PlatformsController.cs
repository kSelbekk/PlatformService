using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dto;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatform()
        {
            Console.WriteLine("---> Getting platforms");
            var platformItems = _repository.GetPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        [HttpGet("{id}", Name = "GetPlatFormById")]
        public ActionResult<PlatformReadDto> GetPlatFormById(int id)
        {
            Console.WriteLine("--- > Getting platform");

            var platform = _repository.GetPlatformById(id);
            if (platform is not null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platform)
        {
            Console.WriteLine("---> Creating platform");

            var createdPlatform = _mapper.Map<Platform>(platform);

            _repository.CreatePlatform(createdPlatform);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(createdPlatform);

            return CreatedAtRoute(nameof(GetPlatFormById), new {id = platformReadDto.Id}, platformReadDto);
        }
    }
}
