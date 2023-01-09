using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Core;
using Code.Scripts.Units;
using UnityEngine;

namespace Code.Scripts.Services
{
    [Serializable]
    public class FormationService
    {
        private Group _group;
        
        [Header("Formation settings")] 
        [SerializeField] private int _baseOrbitUnitAmount;
        [SerializeField] private int _orbitUnitIncreaseAmount;
        [Range(0, 360)] 
        [SerializeField] private int _orbitAngleOffset;
        [SerializeField] private float _orbitOffset;

        private List<GroupUnit> _cachedUnits = new List<GroupUnit>();
        public int OrbitsAmount => Mathf.CeilToInt((_cachedUnits.Count - 1.0f) / _baseOrbitUnitAmount);
        public float OrbitOffset => _orbitOffset;
        
        public void Initialize(Group group)
        {
            _group = group;
        }

        public void Form(List<GroupUnit> units)
        {
            _cachedUnits = units;
            if (_cachedUnits.Count.Equals(0)) return;

            PlaceCenterUnit();
            PlaceOrbitalUnits();
        }

        private void PlaceOrbitalUnits()
        {
            var orbitsPlaced = 0;
            while (orbitsPlaced < OrbitsAmount)
            {
                PlaceOrbitalUnit(orbitsPlaced);
                orbitsPlaced++;
            }
        }

        private void PlaceOrbitalUnit(int orbitNumber)
        {
            var orbitUnitsAmount = _baseOrbitUnitAmount + orbitNumber * _orbitUnitIncreaseAmount;
            var units = _cachedUnits.Skip(ComputeUnitsBeforeOrbit(orbitNumber)).Take(orbitUnitsAmount).ToList();

            for (var i = 0; i < units.Count; i++)
            {
                var unitPosition = ComputeUnitPosition(orbitUnitsAmount, orbitNumber, i);

                var unit = units[i];
                unit.TargetPosition = unitPosition;
            }
        }

        private void PlaceCenterUnit()
        {
            var centerPosition = _group._parentTransform.position;
            var unit = _cachedUnits[0];
            unit.TargetPosition = centerPosition;
        }

        private int ComputeUnitsBeforeOrbit(int layerNumber)
        {
            var amount = 1;
            for (var i = 0; i < layerNumber; i++)
                amount += _baseOrbitUnitAmount + i * 4;

            return amount;
        }

        private Vector3 ComputeUnitPosition(float orbitUnitsAmount, int orbitNumber, int unitIndex)
        {
            var angleIncrement = 360.0f / orbitUnitsAmount;
            var basePosition = _group._parentTransform.position;
            var degreesOffset = orbitNumber * _orbitAngleOffset;
            var angle = Mathf.Deg2Rad * (angleIncrement * unitIndex + degreesOffset);

            return new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle))
                   * (_orbitOffset * (orbitNumber + 1.0f))
                   + basePosition;
        }
    }
}