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
                this._context.LightPoint.Add(homeLightSystem.LightPoints.AsList()
                    .Where(lp =>lp.Id.Equals(lightPointId))
                    .FirstOrDefault());
                this._context.HomeLightSystem.Update(homeLightSystem);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void ChangeLighPointAvailability(HomeLightSystem homeLightSystem)
        {
            try
            {
                this._context.HomeLightSystem.Update(homeLightSystem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task<HomeLightSystem> GetByIdAsync(Guid homeLightSystemId)
        {
            return await _context.HomeLightSystem
                  .IncludePaths("lightPoints")
                .SingleAsync(hls => hls.Id.Equals(homeLightSystemId));
        }

        public async Task<LightPoint> GetLightPointByIdAsync(Guid lightPointId)
        {
            return await _context.LightPoint
                .SingleAsync(hls => hls.Id.Equals(lightPointId));
        }


        public async Task<LightBulb> GetLightBulbByIdAsync(Guid lightBulbId)
        {
            return await _context.LightBulb.SingleAsync(hls => hls.Id.Equals(lightBulbId));
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
