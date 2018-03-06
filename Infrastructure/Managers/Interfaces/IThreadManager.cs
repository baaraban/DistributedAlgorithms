using System;

namespace Infrastructure.Managers.Interfaces
{
    public interface IThreadManager
    {
        int MaxSize { get; }

        void ScheduleAction(Action a);
        void WaitAll();
    }
}
