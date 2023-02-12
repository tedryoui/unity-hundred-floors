using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Core
{
    [Serializable]
    public abstract class Task
    {

        public Action OnComplete;
        protected List<ITaskable> _taskables;
        
        public abstract List<ITaskable> GetTaskables();
        public void Initialize()
        {
            _taskables = GetTaskables();

            foreach (var taskable in _taskables)
            {
                taskable.OnTaskProgressed += ProcessTask;
            }
        }

        protected abstract void ProcessTask(ITaskable taskable);
    }
}