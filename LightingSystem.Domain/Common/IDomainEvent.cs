using System;

namespace LightingSystem.Domain.Common
{
    public interface IDomainEvent 
        //: INotification
    {
        DateTime OccurredOn { get; }
    }
}
