using Infrastructure.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Managers.Implementations
{
    public class SystemThreadingThreadManager : IThreadManager
    {
        private List<Thread> currentRunning;

        public SystemThreadingThreadManager(int maxSize)
        {
            this.MaxSize = maxSize;
            this.currentRunning = new List<Thread>(maxSize);
        }
        public int MaxSize {
            get;
        }

        public void ScheduleAction(Action a)
        {
            throw new NotImplementedException();
        }

        public void WaitAll()
        {
            throw new NotImplementedException();
        }
    }
}
