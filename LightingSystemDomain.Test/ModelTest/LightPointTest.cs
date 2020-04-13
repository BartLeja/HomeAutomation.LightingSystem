using LightingSystem.Domain.HomeLightSystem;
using System;
using System.Linq;
using Xunit;

namespace LightingSystemDomain.Test.ModelTest
{
    public class LightPointTest
    {
        private LightPoint _lightPoint;
        private Guid _lightPointId = Guid.NewGuid();
        private Guid _lightBulbId = Guid.NewGuid();
        private int _numerOfBulbs = 2;
        private string _customName = "first light point in Mr. Putin apartment";

        public LightPointTest() => _lightPoint = new LightPoint(_lightPointId, _customName, _numerOfBulbs);

        [Fact]
        public void AddLightBulb_AddLightBulbToLightPoint_ShouldIncreaseNumberOfBulbsInLightPoint()
        {
            var lightBulb = new LightBulb(_lightBulbId);
            _lightPoint.AddLightBulb(lightBulb);
            
            Assert.Single(_lightPoint.LightBulbs);
        }

        [Fact]
        public void Disable_DisableLightPoint_ShouldChangeIsAvailablePropertyToFalse()
        {
            _lightPoint.Disable();

            Assert.False(_lightPoint.IsAvailable);
        }

        [Fact]
        public void Enable_EnableLightPoint_ShouldChangeIsAvailablePropertyToTrue()
        {
            _lightPoint.Disable();

            Assert.False(_lightPoint.IsAvailable);

            _lightPoint.Enable();

            Assert.True(_lightPoint.IsAvailable);
        }

        [Fact]
        public void ChangeBulbStatus_ChangeBulbStatusOfSelectedLightBulb_StatusOfSelectedLightBulbShouldBeTrue()
        {
            var lightBulb = new LightBulb(_lightBulbId);
            _lightPoint.AddLightBulb(lightBulb);

            Assert.Single(_lightPoint.LightBulbs);

            _lightPoint.ChangeBulbStatus(_lightBulbId, true);

            Assert.True(_lightPoint.LightBulbs.First().Status);
        }
    }
}
