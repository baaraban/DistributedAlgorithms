using Infrastructure.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Infrastructure.Managers.Implementations
{
    public class ThreadingActionQueue : IParallelActionQueue
    {
        private object locker = new object();
        private Queue<Action> actionQueue;
        private List<Thread> currentRunning;

        private void execute()
        {
            while (true)
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

        private void initializeThreads()
        {
            for (var i = 0; i < this.MaxSize; ++i)
            {
                currentRunning.Add(new Thread(() => this.execute()));
                this.currentRunning[i].Name = i.ToString();
            }
        }

        private void startThreads()
        {
            foreach (var t in currentRunning)
            {
                t.Start();
            }
        }

        public int MaxSize { get; }

        public ThreadingActionQueue(int amountOfThreads)
        {
            MaxSize = amountOfThreads;
            actionQueue = new Queue<Action>();
            currentRunning = new List<Thread>();
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
            initializeThreads();
            startThreads();
        }

        public void SynchronizeQueue()
        {
            while (actionQueue.Count != 0) { }
            foreach (var t in this.currentRunning) {
                t.Abort();
            }
            this.currentRunning = new List<Thread>();
        }
    }
}
