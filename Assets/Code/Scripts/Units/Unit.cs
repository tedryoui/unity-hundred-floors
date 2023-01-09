using System;
using UnityEngine;

namespace Code.Scripts.Units
{
    [Serializable]
    [CreateAssetMenu(menuName = "Units/New Unit", order = 0, fileName = "Unit")]
    public class Unit : ScriptableObject, IComparable
    {
        public Unit next;
        public GameObject prefab;
        public int points;
        public float triggerRadius;

        public int CompareTo(object obj)
        {
            if (obj is Unit unit)
            {
                if (points > unit.points)
                    return 1;
                if (points == unit.points)
                    return 0;
                return -1;
            }
            return 0;
        }
    }
}