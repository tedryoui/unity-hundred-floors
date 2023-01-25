using UnityEngine;

namespace Code.Scripts.Stats.Buffs
{
    [CreateAssetMenu(menuName = "Buffs/Types/Percentage")]
    public class PercentageBuffValue : BuffValue
    {
        public override float Process(float baseValue, float prev)
        {
            return prev * baseValue;
        }
    }
}