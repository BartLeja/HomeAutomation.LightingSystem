using AutoMapper;
using Dapper;
using LightingSystem.Data.Dapper;
using LightingSystem.Data.Dtos;
using LightingSystem.Data.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.API.Features.LightPoint.LightPointDataQuery
{
    public class GetLightPointQueryHandler : IRequestHandler<GetLightPointQuery, LightPointDto>
    {
        private readonly IHomeLightSystemRepository _homeLightSystemRepository;
        private readonly IMapper _mapper;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetLightPointQueryHandler(IHomeLightSystemRepository homeLightSystemRepository, IMapper mapper,
             ISqlConnectionFactory sqlConnectionFactory)
        {
            _homeLightSystemRepository = homeLightSystemRepository;
            _sqlConnectionFactory = sqlConnectionFactory;
            _mapper = mapper;
        }

        public async Task<LightPointDto> Handle(GetLightPointQuery request, CancellationToken cancellationToken)
        {
            using (var connection = this._sqlConnectionFactory.GetOpenConnection())
            {
                try
                {
                    const string sql = "SELECT * FROM public.lightpoint WHERE id = @id";
                    var lightPoint = await connection.QueryFirstAsync<LightPointDto>(sql, new { id = request.Id });

                    return lightPoint;
                }
                catch (Exception ex)
                {
                    return new LightPointDto();
                }
            }
        }
    }
}
