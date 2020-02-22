using AutoMapper;
using Dapper;
using LightingSystem.Data.Dapper;
using LightingSystem.Data.Dtos;
using LightingSystem.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.API.Queries
{
    public class GetLightingSystemQueryHandler : IRequestHandler<GetLightingSystemQuery, HomeLightSystemDto>
    {
        private readonly IHomeLightSystemRepository _homeLightSystemRepository;
        private readonly IMapper _mapper;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetLightingSystemQueryHandler(IHomeLightSystemRepository homeLightSystemRepository, IMapper mapper,
             ISqlConnectionFactory sqlConnectionFactory)
        {
            _homeLightSystemRepository = homeLightSystemRepository;
            _sqlConnectionFactory = sqlConnectionFactory;
            _mapper = mapper;
        }
        public async Task<HomeLightSystemDto> Handle(GetLightingSystemQuery request, CancellationToken cancellationToken)
        {
            HomeLightSystemDto homeLightSystemDto = null;
            using (var connection = this._sqlConnectionFactory.GetOpenConnection())
            {
                const string sql = "SELECT * FROM public.homelightsystem WHERE locallightingsystemid = @homeLightSystemId";
                var homeLightSystemId = request.LocalLightingSystemId;
               
                homeLightSystemDto = await connection.QueryFirstAsync<HomeLightSystemDto>(sql, new { homeLightSystemId });

                var homeLightSystemLocalLightingSystemId = request.LocalLightingSystemId;
                
                var lookup = new Dictionary<Guid, LightPointDto>();
                connection.Query<LightPointDto, BulbDto, LightPointDto>(@"
                        SELECT lp.*, b.*
                        FROM public.lightpoint lp 
                        INNER JOIN public.bulb b ON lp.lightpointid = b.lightpointid 
                        AND lp.homelightsystemlocallightingsystemid = @homeLightSystemLocalLightingSystemId", 
                        (lp, b) => {
                    LightPointDto lightPoint;
                    if (!lookup.TryGetValue(lp.LightPointId, out lightPoint))
                        lookup.Add(lp.LightPointId, lightPoint = lp);
                    if (lightPoint.LightBulbs == null)
                        lightPoint.LightBulbs = new List<BulbDto>();
                    lightPoint.LightBulbs.Add(b); /* Add locations to course */
                    return lightPoint;
                }, new { homeLightSystemLocalLightingSystemId }, splitOn: "lightpointid,lightpointid").AsQueryable();
                homeLightSystemDto.LightPoints = lookup.Values.ToList();
            }

            if (homeLightSystemDto == null)
            {
                Console.WriteLine("home is not a home");
            }
            return homeLightSystemDto;
        }
    }
}
