using System;
using System.Linq;
using UnityEngine;

namespace Code.Scripts.Core
{
    [Serializable]
    public class StaticGroup : Group
    {
        [SerializeField] public Formation formation;
        public override Formation Formation => formation;
        public override float GroupSize => Formation.FormationSize;

        public override void Initialize(Transform parentTransform)
        {
            base.Initialize(parentTransform);

            OnChange += MoveUnits;
            
            Formation.Form(this);
        }

        public override void Update()
        {
        }

        protected override void SortUnitsList() => _units = _units.OrderByDescending(x => x.Preset.points).ToList();

        protected override void ReactGroupOverflow()
        {
        }
        
        private void MoveUnits()
        {
            foreach (var unit in _units)
            {
                unit.GetNavMeshAgent.Warp(unit.targetPosition);
            }
        }

    }
}