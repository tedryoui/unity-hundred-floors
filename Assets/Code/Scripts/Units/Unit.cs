using System;
using UnityEngine;

namespace Code.Scripts.Units
{
    [Serializable]
    [CreateAssetMenu(menuName = "Units/New Unit", order = 0, fileName = "Unit")]
    public class Unit : ScriptableObject
    {
        public Unit nextLevel;
        public GameObject prefab;
        public int points;
        public float triggerRadius;
    }
}