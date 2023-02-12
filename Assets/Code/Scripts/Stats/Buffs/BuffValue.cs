using System;
using UnityEngine;

namespace Code.Scripts.Stats.Buffs
{
    public abstract class BuffValue : ScriptableObject
    {
        public abstract float Process(float baseValue, float prev);
    }
}