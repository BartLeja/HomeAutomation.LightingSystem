using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LightingSystem.API.Commands;
using LightingSystem.API.Features.LightPoint.Create;
using LightingSystem.API.Features.LightPoint.Delete;
using LightingSystem.API.Features.LightPoint.Disable;
using LightingSystem.API.Features.LightPoint.LightPointDataQuery;
using LightingSystem.Data.Dtos;
using LightingSystem.Domain.HomeLightSystem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LightingSystem.API.Features.LightPoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class LightPointController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LightPointController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        // GET: api/LightPoint/4a3b39c7-ac56-4853-900f-b776c10cc2e3
        [HttpGet("{id}")]
        public async Task<LightPointDto> Get(Guid id)
        {
            return await _mediator.Send(new GetLightPointQuery(id));
        }

        // POST: api/LightPoint
        [HttpPost("{homeLightSystemId}")]
        public async Task<IActionResult> PostLightPoint(Guid homeLightSystemId, [FromBody] LightPointDto request)
        {
            var bulbs = new List<LightBulb>();

            bulbs = _mapper.Map<List<LightBulb>>(request.LightBulbs);
            var lightPoint = await _mediator.Send(
                new AddLightPointCommand(
                    request.Id,
                    request.CustomName,
                    homeLightSystemId,
                    bulbs));
            // TODO mapper
            return Created(string.Empty, lightPoint);
        }

        // POST: api/LightPoint/disableLightPoint/4a3b39c7-ac56-4853-900f-b776c10cc2e3
        [HttpPost("disableLightPoint/{lightPointId}")]
        public async Task<IActionResult> DisableLightPoint(Guid lightPointId)
        {
            await _mediator.Send(new DisableLighPointCommand(lightPointId));
            return Ok(lightPointId);
        }

        // POST: api/LightPoint/enableLightPoint/4a3b39c7-ac56-4853-900f-b776c10cc2e3
        [HttpPost("enableLightPoint/{lightPointId}")]
        public async Task<IActionResult> EnableLightPoint(Guid lightPointId)
        {
            await _mediator.Send(new EnableLightPointCommand(lightPointId));
            return Ok(lightPointId);
        }

        // DELETE: api/LightPoint/4a3b39c7-ac56-4853-900f-b776c10cc2e3
        [HttpDelete("{lightPointId}")]
        public async Task<IActionResult> DeleteLightPoint(Guid lightPointId)
        {
            await _mediator.Send(new DeleteLightPointCommand(lightPointId));
            return Ok(lightPointId);
        }
    }
}