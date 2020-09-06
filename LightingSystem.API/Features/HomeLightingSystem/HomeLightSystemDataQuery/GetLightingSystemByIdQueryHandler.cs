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

namespace LightingSystem.API.Features.HomeLightingSystem.HomeLightSystemDataQuery
{
    public class GetLightingSystemByIdQueryHandler : IRequestHandler<GetLightingSystemByIdQuery, HomeLightSystemDto>
    {
        private readonly IMapper _mapper;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetLightingSystemByIdQueryHandler(
             IMapper mapper,
             ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _mapper = mapper;
        }
        public async Task<HomeLightSystemDto> Handle(GetLightingSystemByIdQuery request, CancellationToken cancellationToken)
        {
            HomeLightSystemDto homeLightSystemDto = null;
            using (var connection = _sqlConnectionFactory.GetOpenConnection())
            {
                const string sql = "SELECT * FROM public.homelightsystem WHERE id = @homeLightSystemId";
                var homeLightSystemId = request.LocalLightingSystemId;
               
                homeLightSystemDto = await connection.QueryFirstAsync<HomeLightSystemDto>(sql, new { homeLightSystemId });

                //var homeLightSystemLocalLightingSystemId = request.LocalLightingSystemId;
                
                var lookup = new Dictionary<Guid, LightPointDto>();
                connection.Query<LightPointDto, LightBulbDto,LightsGroupDto, LightPointDto>(@"
                        SELECT lp.*, b.*, lg.*
                        FROM public.lightpoint lp 
                        INNER JOIN public.lightbulb b ON lp.id = b.lightpointid 
                        LEFT JOIN public.lightsgroup lg ON lp.lightsgroupid = lg.id       
                        AND lp.homelightsystemid = @homeLightSystemId", 
                        (lp, b, lg) => {
                    LightPointDto lightPoint;
                    if (!lookup.TryGetValue(lp.Id, out lightPoint))
                            {
                                if (lg!=null)
                                {
                                    lp.LightsGroup = lg;
                                }
                               
                                lookup.Add(lp.Id, lightPoint = lp);
                            }
                    if (lightPoint.LightBulbs == null)
                        lightPoint.LightBulbs = new List<LightBulbDto>();
                    lightPoint.LightBulbs.Add(b); /* Add locations to course */
                    return lightPoint;
                }, new { homeLightSystemId }, splitOn: "id,id,id,id").AsQueryable();
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
