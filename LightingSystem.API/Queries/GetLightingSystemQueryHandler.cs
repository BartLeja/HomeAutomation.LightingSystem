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
                const string sql = "SELECT * FROM public.homelightsystem WHERE id = @homeLightSystemId";
                var homeLightSystemId = request.LocalLightingSystemId;
               
                homeLightSystemDto = await connection.QueryFirstAsync<HomeLightSystemDto>(sql, new { homeLightSystemId });

                //var homeLightSystemLocalLightingSystemId = request.LocalLightingSystemId;
                
                var lookup = new Dictionary<Guid, LightPointDto>();
                connection.Query<LightPointDto, LightBulbDto, LightPointDto>(@"
                        SELECT lp.*, b.*
                        FROM public.lightpoint lp 
                        INNER JOIN public.lightbulb b ON lp.id = b.lightpointid 
                        AND lp.homelightsystemid = @homeLightSystemId", 
                        (lp, b) => {
                    LightPointDto lightPoint;
                    if (!lookup.TryGetValue(lp.Id, out lightPoint))
                        lookup.Add(lp.Id, lightPoint = lp);
                    if (lightPoint.LightBulbs == null)
                        lightPoint.LightBulbs = new List<LightBulbDto>();
                    lightPoint.LightBulbs.Add(b); /* Add locations to course */
                    return lightPoint;
                }, new { homeLightSystemId }, splitOn: "id,id,id").AsQueryable();
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
