﻿using Dapper;
using LightingSystem.Data.Dapper;
using LightingSystem.Data.EntityConfigurations;
using LightingSystem.Data.Extensions;
using LightingSystem.Domain.HomeLightSystem;
using LightingSystem.Domain.Models;
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
                  .Include(hls => hls.LightPoints)
                  .ThenInclude(lp => lp.LightBulbs)
                  .Include(hls => hls.LightPoints)
                  .ThenInclude(lp =>lp.LightsGroup)
                .SingleAsync(hls => hls.Id.Equals(homeLightSystemId));
        }

        public async Task<LightPoint> GetLightPointByIdAsync(Guid lightPointId)
        {
            return await _context.LightPoint
                .Include(lp=>lp.LightBulbs)
                .SingleAsync(hls => hls.Id.Equals(lightPointId));
        }


        public async Task<LightBulb> GetLightBulbByIdAsync(Guid lightBulbId)
        {
            return await _context.LightBulb.SingleAsync(hls => hls.Id.Equals(lightBulbId));
        }

        public async Task DeleteLighPoint(Guid lightPointId)
        {
            try
            {
                // TODO change for DDD approche when we will have message bus 
               var lightPointToRemove = await _context.LightPoint.Where(lp => lp.Id.Equals(lightPointId)).FirstOrDefaultAsync();
                _context.LightPoint.Remove(lightPointToRemove);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task CreateLightsGroup(LightsGroup lightsGroup)
        {
            try
            {
               await _context.LightsGroup.AddAsync(lightsGroup);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task<LightsGroup> CheckIfLightsGroupExist(string lightGroupName)
        {
            return await _context.LightsGroup.FirstOrDefaultAsync(lg => lg.LightGroupName == lightGroupName);
        }

        public async Task UpdateLightPoint(LightPoint lightPoint)
        {
            try
            {
                _context.LightPoint.Update(lightPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task UpdateHomeLightSystem(HomeLightSystem homeLightSystem)
        {
            try
            {
                _context.HomeLightSystem.Update(homeLightSystem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task<LightsGroup> GetLightsGroupByIdAsync(Guid LightsGroupId)
        {
                return await _context.LightsGroup
                    .SingleAsync(hls => hls.Id.Equals(LightsGroupId));
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
