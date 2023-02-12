using System.Collections;
using UnityEngine;

namespace Code.Scripts.Stats.Buffs
{
    [CreateAssetMenu(menuName = "Buffs/Streams/Invulnerable")]
    public class InvulnerableBuffStream : BuffStream
    {
        public override IEnumerator Stream(Buff buff)
        {
            yield return new WaitWhile(() => true);
        }
    }
}