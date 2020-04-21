using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LightingSystem.API.Features.HomeLightingSystem.Create;
using LightingSystem.API.Features.HomeLightingSystem.DisableAllLightPoints;
using LightingSystem.API.Features.HomeLightingSystem.EnableAllLightPoints;
using LightingSystem.API.Features.HomeLightingSystem.HomeLightSystemDataQuery;
using LightingSystem.API.Features.HomeLightingSystem.HomeLightSystemsDataQuery;
using LightingSystem.Data.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LightingSystem.API.Features.HomeLightingSystem
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class HomeLightSystemController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public HomeLightSystemController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        // GET: api/LightSystem
        [HttpGet]
        public async Task<List<HomeLightSystemDto>> Get()
        {
            return await _mediator.Send(new GetLightingSystemsQuery());
        }

        // GET: api/HomeLightSystem/5
        [HttpGet("{id}")]
        public async Task<HomeLightSystemDto> Get(Guid id)
        {
            return await _mediator.Send(new GetLightingSystemByIdQuery(id));
        }

        // POST: api/HomeLightSystem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HomeLightSystemCommandDto request)
        {
            var homeLightSystem = await _mediator.Send(new AddLightSystemCommand(request.UserName));
            return Created(string.Empty, homeLightSystem);
        }

        // POST: api/HomeLightSystem/disableAllLightPoints/4a3b39c7-ac56-4853-900f-b776c10cc2e3
        [HttpPost("disableAllLightPoints/{homeLightSystemid}")]
        public async Task<IActionResult> DisableAllLightPoints(Guid homeLightSystemId)
        {
            await _mediator.Send(new DisableAllLightPointsCommand(homeLightSystemId));
            return Ok(homeLightSystemId);
        }

        // POST: api/HomeLightSystem/enableAllLightPoints/4a3b39c7-ac56-4853-900f-b776c10cc2e3
        [HttpPost("enableAllLightPoints/{homeLightSystemid}")]
        public async Task<IActionResult> EnableAllLightPoints(Guid homeLightSystemId)
        {
            await _mediator.Send(new EnableAllLightPointsCommand(homeLightSystemId));
            return Ok(homeLightSystemId);
        }
    }
}
