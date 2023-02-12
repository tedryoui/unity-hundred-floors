using System;
using Code.Scripts.Stats.Buffs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Scripts.Stats
{
    [Serializable]
    public class InvulnerableStat : Stat
    {
        public bool IsVulnerable => (_singleBuff == null);
        
        public override float GetValue => _baseValue;

        private Buff _singleBuff;
        
        public override void AddStatBuff(Buff buff)
        {
            base.AddStatBuff(buff);

            _singleBuff = Object.Instantiate(buff);
        }

        protected override void RemoveStatBuff(Buff buff)
        {
            Object.Destroy(_singleBuff);
            _singleBuff = null;
        }
    }
}