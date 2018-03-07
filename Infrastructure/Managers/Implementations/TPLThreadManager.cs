using Infrastructure.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Managers.Implementations
{
    public class TPLThreadManager: IThreadManager
    {
        private object locker = new object();
        private Queue<Action> actionQueue;
        private List<Task> currentRunning;

        private bool canRunParallel {
            get {
                lock (locker)
                {
                    return currentRunning.Count < MaxSize;
                }
            }
        }

        private Task initializeTask(Action a)
        {
            lock (locker)
            {
                var task = new Task(a);
                task.ContinueWith((t) =>
                {
                    this.proceedExecution();
                });
                this.currentRunning.Add(task);
                return task;
            }
        }

        private void proceedExecution()
        {
            lock (locker)
            {
                this.currentRunning = this.currentRunning.Where(t => !t.IsCompleted || t == null).ToList();
                if (actionQueue.Count == 0) return;
                if (!canRunParallel) return;
                initializeTask(this.actionQueue.Dequeue()).Start();
            }
        }

        public int MaxSize { get; }

        public TPLThreadManager(int amountOfThreads)
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
                if (canRunParallel)
                {
                    initializeTask(actionQueue.Dequeue()).Start();
                    return;
                }
            }
        }

        public void WaitAll()
        {
            while(actionQueue.Count != 0){}
            lock (locker)
            {
                Task.WaitAll(this.currentRunning.ToArray());
                this.currentRunning = new List<Task>();
            }
        }
    }
}
