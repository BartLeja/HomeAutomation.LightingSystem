using LightingSystem.Domain.Common;
using System;

namespace LightingSystem.Domain.HomeLightSystem
{
    public class Bulb : Entity
    {
        public Guid Id { get; set; }
        private int number;
        private bool status;

        public Bulb(int number)
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
