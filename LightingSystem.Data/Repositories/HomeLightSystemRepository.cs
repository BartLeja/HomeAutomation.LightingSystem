using LightingSystem.Infrastructure.Database;
using LightingSystem.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LightingSystem.Data.Repositories
{
    public class HomeLightSystemRepository: IHomeLightSystemRepository
    {
        private readonly HomeLightSystemContext _context;

        public HomeLightSystemRepository(HomeLightSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(HomeLightSystem homeLightSystem)
        {
             await this._context.HomeLightSystem.AddAsync(homeLightSystem);
        }

        public async Task<HomeLightSystem> GetByIdAsync(Guid homeLightSystemId)
        {
            return await _context.HomeLightSystem
                .Include(hls => hls.LightPoints)
                .ThenInclude(lp => lp.Bulbs)
                .SingleAsync(hls => hls.LocalLightingSystemId == homeLightSystemId);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
