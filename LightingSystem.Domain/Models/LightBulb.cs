using LightingSystem.Domain.Common;
using System;

namespace LightingSystem.Domain.HomeLightSystem
{
    public class LightBulb : Entity
    {
        public Guid Id { get; set; }
        private int number;
        private bool status;

        public LightBulb(int number)
        {
            this.number = number;
            status = false;
        }

        public void ChangeStatus(bool status)
        {
            this.status = status;
        }
    }
}
