using AutoMapper;
using Dapper;
using LightingSystem.Data.Dapper;
using LightingSystem.Data.EntityConfigurations;
using LightingSystem.Data.Extensions;
using LightingSystem.Domain.HomeLightSystem;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LightingSystem.Data.Repositories
{
    public class HomeLightSystemRepository: IHomeLightSystemRepository
    {
        private readonly HomeLightSystemContext _context;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public HomeLightSystemRepository(
            HomeLightSystemContext context, 
            ISqlConnectionFactory sqlConnectionFactory
            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task AddAsync(HomeLightSystem homeLightSystem)
        {
             await this._context.HomeLightSystem.AddAsync(homeLightSystem);
        }

        public void AddLightPoint(HomeLightSystem homeLightSystem, Guid lightPointId)
        {
            try
            {
                // _context.Entry(homeLightSystem).State = EntityState.Modified;
                //var test = await this._context.HomeLightSystem.Include(hls => hls.LightPoints).FirstAsync();
                //  test.LightPoints.
                //var test = homeLightSystem.LightPoints.AsList()[0];
                //this._context.LightPoint.Add(test);
                this._context.LightPoint.Add(homeLightSystem.LightPoints.AsList()
                    .Where(lp =>lp.LightPointId.Equals(lightPointId))
                    .FirstOrDefault());
                this._context.HomeLightSystem.Update(homeLightSystem);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async  Task<HomeLightSystem> GetByIdAsync(Guid homeLightSystemId)
        {
            //HomeLightSystem homeLightSystem;
            //using (var connection = this._sqlConnectionFactory.GetOpenConnection())
            //{
            //    const string sql = "SELECT * FROM public.homelightsystem WHERE locallightingsystemid = @homeLightSystemId";

            //    //var homeLightSystem = await connection.QuerySingleOrDefaultAsync<HomeLightSystem>(sql, new { homeLightSystemId });
            //    homeLightSystem = await connection.QueryFirstAsync<HomeLightSystem>(sql, new { homeLightSystemId });

            //    const string sqlProducts = "SELECT FROM  public.LightPoint WHERE HomeLightSystemLocalLightingSystemId = @homeLightSystemId";
            //    var lightPoints = await connection.QueryFirstAsync<LightPoint>(sql, new { homeLightSystemId });

            //    homeLightSystem.LightPoints = lightPoints;

            //    //order.Products = products.AsList();

            //}

            //return homeLightSystem;

            //return await _context.HomeLightSystem
            //    .Include(hls => hls.LightPoints)
            //    .ThenInclude(lp => lp.LightBulbs)
            //    .FirstOrDefaultAsync(hls => hls.LocalLightingSystemId.Equals(homeLightSystemId));

            return await _context.HomeLightSystem
                  .IncludePaths("lightPoints")
                .SingleAsync(hls => hls.LocalLightingSystemId.Equals(homeLightSystemId));
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
