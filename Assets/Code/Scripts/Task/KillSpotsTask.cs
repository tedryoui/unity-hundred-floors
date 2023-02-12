using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Scripts.Core
{
    public class KillSpotsTask : Task
    {
        public override List<ITaskable> GetTaskables()
        {
            return Object.FindObjectsOfType<Spot>().Select(x => (ITaskable)x).ToList();
        }

        protected override void ProcessTask(ITaskable taskable)
        {
            _taskables.Remove(taskable);

            if (_taskables.Count == 0)
            {
                OnComplete?.Invoke();
            }
        }
    }
}