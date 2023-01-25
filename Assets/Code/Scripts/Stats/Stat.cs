using System;
using System.Collections.Generic;
using Code.Scripts.Core;
using Code.Scripts.Stats.Buffs;
using UnityEngine;

namespace Code.Scripts.Stats
{
    [Serializable]
    public abstract class Stat
    {
        [SerializeField] protected float _baseValue;

        public abstract float GetValue { get; }

        public virtual void AddStatValue(float value)
        {
            _baseValue += value;
        }

        public virtual void RemoveStatValue(float value)
        {
            _baseValue -= value;
        }

        public virtual void AddStatBuff(Buff buff)
        {
            buff.OnCompleted += RemoveStatBuff;
            GameCore.GetPlayer.StartCoroutine(buff.GetStream);
        }

        protected abstract void RemoveStatBuff(Buff buff);
    }
}