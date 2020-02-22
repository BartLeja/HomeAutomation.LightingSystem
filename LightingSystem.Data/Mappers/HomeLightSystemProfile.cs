using AutoMapper;
using LightingSystem.Data.Dtos;
using LightingSystem.Domain.HomeLightSystem;
using System.Collections.Generic;

namespace LightingSystem.Data.Mappers
{
    public class HomeLightSystemMapper: Profile
    {
        public HomeLightSystemMapper()
        {
            ShouldMapField = fieldInfo => true;
            ShouldMapProperty = propertyInfo => true;
            CreateMap<HomeLightSystem, HomeLightSystemDto>();
            CreateMap<LightPoint, LightPointDto>();
            CreateMap<LightBulb, LightBulbDto>();
            CreateMap<LightBulbDto, LightBulb>();
        }
    }
}
