using LightingSystem.Domain.HomeLightSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LightingSystemDomain.Test.ModelTest
{
    public class HomeLightSystemTest
    {
        private HomeLightSystem _homeLightSystem;
        private string _userName = "putinMasterOfTheWorld";
        private List<LightBulb> _lightBulbs;
        private Guid _lightPointId = Guid.NewGuid();
        private string _customName = "first light point in Mr. Putin apartment";

        public HomeLightSystemTest()
        {
            _homeLightSystem = new HomeLightSystem(_userName);
            _lightBulbs = new List<LightBulb>()
            {
                new LightBulb() {Id = Guid.NewGuid() },
                new LightBulb() {Id = Guid.NewGuid()}
            };
        }

        [Fact]
        public void AddLighPoint_AddLightPointToHomeLightSystem_ReturnLightPoitId()
        {
            var result = _homeLightSystem.AddLighPoint(
                _lightPointId,
                _customName,
                _lightBulbs);
           
            Assert.Equal(_lightPointId, result);
            Assert.Equal(2, _homeLightSystem.LightPoints.First().LightBulbs.Count());
            Assert.Equal(_customName, _homeLightSystem.LightPoints.First().CustomName);
        }

        [Fact]
        public void DisableAllLighPoints_DisableAllLighPointsinHomeLightSystem_ShouldDisableAllLightPoint()
        {
            _homeLightSystem.AddLighPoint(
                _lightPointId,
                _customName,
                _lightBulbs);

            _homeLightSystem.AddLighPoint(
                _lightPointId,
                _customName,
                _lightBulbs);

            _homeLightSystem.DisableAllLighPoints();
            
            Assert.False(_homeLightSystem.LightPoints.First().IsAvailable);
            Assert.False(_homeLightSystem.LightPoints.ElementAt(1).IsAvailable);
        }

        [Fact]
        public void EnableAllLighPoints_EnableAllLighPointsinHomeLightSystem_ShouldEnableAllLightPoint()
        {
          
            _homeLightSystem.AddLighPoint(
                _lightPointId,
                _customName,
                _lightBulbs);

            _homeLightSystem.AddLighPoint(
                _lightPointId,
                _customName,
                _lightBulbs);

            _homeLightSystem.DisableAllLighPoints();

            Assert.False(_homeLightSystem.LightPoints.First().IsAvailable);
            Assert.False(_homeLightSystem.LightPoints.ElementAt(1).IsAvailable);

            _homeLightSystem.EnableAllLighPoints();

            Assert.True(_homeLightSystem.LightPoints.First().IsAvailable);
            Assert.True(_homeLightSystem.LightPoints.ElementAt(1).IsAvailable);
        }
    }
}
