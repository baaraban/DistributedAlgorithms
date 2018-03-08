using System;

namespace Infrastructure.Managers.Interfaces
{
    public interface IParallelActionQueue
    {
        int MaxSize { get; }

        void Start();
        void ScheduleAction(Action a);
        void SynchronizeQueue();
    }
}
