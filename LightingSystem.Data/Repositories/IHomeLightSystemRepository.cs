using LightingSystem.Infrastructure.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LightingSystem.Data.Repositories
{
    public interface IHomeLightSystemRepository
    {
        Task AddAsync(HomeLightSystem homeLightSystem);
        Task<HomeLightSystem> GetByIdAsync(Guid homeLightSystemId);
        Task Save();
    }
}
