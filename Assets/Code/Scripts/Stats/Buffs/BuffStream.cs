using System.Collections;
using UnityEngine;

namespace Code.Scripts.Stats.Buffs
{
    public abstract class BuffStream : ScriptableObject
    {
        public abstract IEnumerator Stream(Buff buff);
    }
}