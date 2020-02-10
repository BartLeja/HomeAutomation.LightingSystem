using LightingSystem.Domain.Common;
using System;

namespace LightingSystem.Domain.HomeLightSystem
{
    public class Bulb : Entity
    {
        public Guid Id { get; set; }
        private bool Status { get; set; }

        public void ChangeStatus(bool status)
        {
            Status = status;
        }
    }
}
