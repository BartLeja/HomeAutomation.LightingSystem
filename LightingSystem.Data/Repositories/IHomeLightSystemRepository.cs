
using LightingSystem.Data.Dtos;
using LightingSystem.Domain.HomeLightSystem;
using LightingSystem.Domain.Models;
using System;
using System.Threading.Tasks;

namespace LightingSystem.Data.Repositories
{
    public interface IHomeLightSystemRepository
    {
        Task AddAsync(HomeLightSystem homeLightSystem);
        Task<HomeLightSystem> GetByIdAsync(Guid homeLightSystemId);
        void AddLightPoint(HomeLightSystem homeLightSystem, Guid lightPointId);
        void ChangeLighPointAvailability(HomeLightSystem homeLightSystem);
        Task<LightBulb> GetLightBulbByIdAsync(Guid lightBulbId);
        Task<LightPoint> GetLightPointByIdAsync(Guid lightPointId);
        Task DeleteLighPoint(Guid lightPointId);
        Task CreateLightsGroup(LightsGroup lightsGroup);
        Task UpdateLightPoint(LightPoint lightPoint);
        Task<LightsGroup> GetLightsGroupByIdAsync(Guid LightsGroupId);
        Task<LightsGroup> CheckIfLightsGroupExist(string lightGroupName);
        Task Save();
    }
}
