using UnityEngine;

namespace Code.Scripts.Stats.Buffs
{
    [CreateAssetMenu(menuName = "Buffs/Types/Plain")]
    public class PlainBuffValue : BuffValue
    {
        public override float Process(float baseValue, float prev)
        {
            return baseValue + prev;
        }
    }
}