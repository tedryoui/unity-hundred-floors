using System;
using System.Linq;
using UnityEngine;

namespace Code.Scripts.Core
{
    [Serializable]
    public class DynamicGroup : Group
    {
        [SerializeField] public Formation formation;
        public override Formation Formation => formation;

        public override void Initialize(Transform parentTransform)
        {
            base.Initialize(parentTransform);
        }

        public override float GroupSize => formation.FormationSize;

        protected override void SortUnitsList() => _units = _units.OrderByDescending(x => x.Preset.points).ToList();

        protected override void ReactGroupOverflow()
        {
        }
    }
}