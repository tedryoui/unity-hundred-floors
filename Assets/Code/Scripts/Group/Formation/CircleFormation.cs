using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Units;
using UnityEngine;

namespace Code.Scripts.Group.Formation
{
    [Serializable]
    [CreateAssetMenu(menuName = "Formation/Circle formation")]
    public class CircleFormation : Core.Formation
    {
        [Header("Formation unit")] 
        [SerializeField] private int _baseOrbitUnitAmount;
        [SerializeField] private int _orbitUnitIncreaseAmount;
        [Range(0, 360)] 
        [SerializeField] private int _orbitAngleOffset;
        [SerializeField] private float _orbitOffset;
        private float _longestRadius;

        private int OrbitsAmount => Mathf.CeilToInt((UnitsReference.Count - 1.0f) / _baseOrbitUnitAmount);
        public override int FormationSize => (_longestRadius <= _orbitOffset) ? 1 : (int)(_longestRadius / _orbitOffset);

        // Formation example
        // The formation consists of few orbits with certainly
        // increasing amount of units
        //                    
        //       0  0  0
        //    0     0     0
        //    0  0  o  0  0
        //    0     0     0
        //       0  0  0
        //  

        protected override void Form()
        {
            _longestRadius = 0.0f;

            if (UnitsReference.Count < 1)
                return;

            PlaceCentralUnit();
            PlaceOrbitalUnits();
        }

        private void PlaceCentralUnit()
        {
            // Get first (the most high by level) unit
            var unit = UnitsReference[0];
        
            // Set unit target to the center of formation (center of the circle)
            unit.targetPosition = FormationPosition;
        }
        
        private void PlaceOrbitalUnits()
        {
            // Filling formation depends on orbits amount
            for (int orbit = 0; orbit < OrbitsAmount; orbit++)
            {
                // Getting set of units to be placed on the certain orbit
                var unitsAmount = _baseOrbitUnitAmount + orbit * _orbitUnitIncreaseAmount;
                var units = UnitsReference
                    .Skip(ComputeUnitsBeforeOrbit(orbit))
                    .Take(unitsAmount)
                    .ToList();
                
                FillOrbit(orbit, units);
            }
        }

        private void FillOrbit(int orbit, List<GroupUnit> units)
        {
            for (var i = 0; i < units.Count; i++)
            {
                // Computing positions on formation for every unit
                var unitPosition = ComputeUnitPosition(units.Count, orbit, i);
                
                // Replace (if needed) longest radius
                var distance = Vector3.Distance(FormationPosition, unitPosition);
                if (_longestRadius < distance)
                    _longestRadius = distance;

                // Setting for each unit his own formation position
                var unit = units[i];
                unit.targetPosition = unitPosition;
            }
        }
        
        // Function for calculating the amount of units before the orbit
        // depending on base unit count, units increase amount and orbits count before
        private int ComputeUnitsBeforeOrbit(int layerNumber)
        {
            var amount = 1;
            for (var i = 0; i < layerNumber; i++)
                amount += _baseOrbitUnitAmount + i * _orbitUnitIncreaseAmount;

            return amount;
        }

        private Vector3 ComputeUnitPosition(float orbitUnitsAmount, int orbitNumber, int unitIndex)
        {
            // Getting angle offset between each unit
            var angleIncrement = 360.0f / orbitUnitsAmount;
            
            var degreesOffset = orbitNumber * _orbitAngleOffset;
            
            // Converting computed before angle amount to the radiant equivalent
            var angle = Mathf.Deg2Rad * (angleIncrement * unitIndex + degreesOffset);

            // Returning the position through transformation of X = Cos(angle) and Y = Sin(angle)
            // offseted by formation center 
            return new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle))
                   * (_orbitOffset * (orbitNumber + 1.0f))
                   + FormationPosition;
        }
    }
}