using System;
using System.Collections;
using UnityEngine;

namespace Code.Scripts.Stats.Buffs
{
    [Serializable]
    [CreateAssetMenu(menuName = "Buffs/Stream/Timer")]
    public class TimerBuffStream : BuffStream
    {
        [SerializeField] private float _timeDelay;
        
        public override IEnumerator Stream(Buff buff)
        {
            yield return new WaitForSeconds(_timeDelay);
            buff.OnCompleted?.Invoke(buff);
        }
    }
}