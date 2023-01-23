using System;

namespace Code.Scripts.Core
{
    public interface ITaskable
    {
        public Action<ITaskable> OnTaskProgressed { get; set; }
    }
}