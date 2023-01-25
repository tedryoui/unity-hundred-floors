using System;
using System.Collections.Generic;
using Code.Scripts.Core;
using Code.Scripts.Stats.Buffs;
using UnityEngine;

namespace Code.Scripts.Stats
{
    [Serializable]
    public class VulnerableStat : Stat
    {
        [SerializeField] private List<Buff> _buffs = new ();

        public override float GetValue
        {
            get
            {
                var completeValue = _baseValue;

                foreach (var buff in _buffs)
                    completeValue = buff.Process(completeValue);
                
                return completeValue;
            }
        }

        public override void AddStatBuff(Buff buff)
        {
            base.AddStatBuff(buff);
            
            _buffs.Add(buff);
        }

        protected override void RemoveStatBuff(Buff buff)
        {
            _buffs.Remove(buff);
        }
    }
}