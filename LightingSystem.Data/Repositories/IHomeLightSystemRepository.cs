
using LightingSystem.Data.Dtos;
using LightingSystem.Domain.HomeLightSystem;
using System;
using System.Threading.Tasks;

namespace LightingSystem.Data.Repositories
{
    public interface IHomeLightSystemRepository
    {
        Task AddAsync(HomeLightSystem homeLightSystem);
        Task<HomeLightSystem> GetByIdAsync(Guid homeLightSystemId);
        void AddLightPoint(HomeLightSystem homeLightSystem, Guid lightPointId);
        Task Save();
    }
}
