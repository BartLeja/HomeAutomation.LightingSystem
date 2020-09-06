using LightingSystem.Domain.Common;
using LightingSystem.Domain.Models;
using System;

namespace LightingSystem.Domain.HomeLightSystem
{
    public class LightBulb : Entity
    {
        public Guid Id { get; set; }
        private bool status;
        public bool Status => status;
   
        public LightBulb() => status = false;

        public LightBulb(Guid Id)
        {
            status = false;
            this.Id = Id;
        }

        public void ChangeStatus(bool status)
        {
            this.status = status;
        }
    }
}
