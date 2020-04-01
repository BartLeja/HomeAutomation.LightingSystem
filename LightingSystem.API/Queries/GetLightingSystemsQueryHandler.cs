using AutoMapper;
using Dapper;
using LightingSystem.Data.Dapper;
using LightingSystem.Data.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.API.Queries
{
    public class GetLightingSystemsQueryHandler : IRequestHandler<GetLightingSystemsQuery, List<HomeLightSystemDto>>
    {
        private readonly IMapper _mapper;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetLightingSystemsQueryHandler(
             IMapper mapper,
             ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _mapper = mapper;
        }

        public async Task<List<HomeLightSystemDto>> Handle(GetLightingSystemsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<HomeLightSystemDto> homeLightSystemDto = null;
            using (var connection = this._sqlConnectionFactory.GetOpenConnection())
            {
                const string sql = "SELECT * FROM public.homelightsystem";
              
                homeLightSystemDto = await connection.QueryAsync<HomeLightSystemDto>(sql);
               
                homeLightSystemDto.ToList().ForEach(hls =>
                {
                    var lookup = new Dictionary<Guid, LightPointDto>();
                    var homeLightSystemId = hls.Id;
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
                    hls.LightPoints = lookup.Values.ToList();
                });
               
            }

            if (homeLightSystemDto == null)
            {
                Console.WriteLine("home is not a home");
            }
            return homeLightSystemDto.ToList();
        }
    
    }
}

