using Infrastructure.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Managers.Implementations
{
    public class TPLActionQueue : IParallelActionQueue
    {
        private object locker = new object();
        private Queue<Action> actionQueue;
        private List<Task> currentRunning;
        private Boolean isRunning = false;

        private void execute()
        {
            while (isRunning)
            {
                Action toExecute = null;
                lock (locker)
                {
                    if (actionQueue.Count > 0)
                    {
                        toExecute = actionQueue.Dequeue();
                    }
                }
                toExecute?.Invoke();
            }
        }
        private void initializeTasks()
        {
            for (var i = 0; i < this.MaxSize; ++i)
            {
                currentRunning.Add(new Task(() => this.execute()));
            }
        }

        private void startTasks()
        {
            foreach (var t in currentRunning)
            {
                t.Start();
            }
        }

        public int MaxSize { get; }

        public TPLActionQueue(int amountOfThreads)
        {
            MaxSize = amountOfThreads;
            actionQueue = new Queue<Action>();
            currentRunning = new List<Task>();
        }

        public void ScheduleAction(Action a)
        {
            lock (locker)
            {
                actionQueue.Enqueue(a);
            }
        }

        public void Start()
        {
            this.isRunning = true;
            initializeTasks();
            startTasks();
        }

        public void SynchronizeQueue()
        {
            while (actionQueue.Count != 0) { }
            isRunning = false;
            Task.WaitAll(this.currentRunning.ToArray());
            this.currentRunning = new List<Task>();
        }
    }
}
