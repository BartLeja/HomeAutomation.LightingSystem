using LightingSystem.Domain.HomeLightSystem;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LightingSystemDomain.Test.ModelTest
{
    public class LightBulbTest
    {
        private LightBulb _lightBulb;

        public LightBulbTest()
        {
            _lightBulb = new LightBulb();
        }

        [Fact]
        public void ChangeStatus_ChangeStatusOfLightBulb_ShouldChangeStatusForTrue()
        {
            Assert.False(_lightBulb.Status);

            _lightBulb.ChangeStatus(true);

            Assert.True(_lightBulb.Status);
        }
    }
}
