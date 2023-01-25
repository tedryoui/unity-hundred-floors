using System;
using System.Collections;
using UnityEngine;

namespace Code.Scripts.Stats.Buffs
{
    [Serializable]
    [CreateAssetMenu(menuName = "Buffs/New Buff", fileName = "Buff", order = 0)]
    public class Buff : ScriptableObject
    {
        public float BaseValue;
        
        [SerializeField] private BuffValue _value;
        [SerializeField] private BuffStream _stream;
        
        public Action<Buff> OnCompleted;

        public float Process(float prev) => _value.Process(BaseValue, prev);
        public IEnumerator GetStream => _stream.Stream(this);
    }
}