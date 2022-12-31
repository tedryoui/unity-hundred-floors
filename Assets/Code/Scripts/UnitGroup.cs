using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Scripts
{
    [Serializable]
    public class UnitGroup
    {
        private Transform _playerTransform;
        // Add one
        // Add few
        // Remove one
        // Remove few
        // Merge
        [SerializeField] private List<SceneUnit> _units = new();
        [SerializeField] private int _baseOrbitUnitAmount;
        [SerializeField] private int _orbitUnitInscreaseAmount;
        [SerializeField] private int _orbitAngleOffset;
        [SerializeField] private float _orbitOffset;

        //[SerializeField] private Formation formation;

        public void Initialize(Transform playerTransform)
        {
            this._playerTransform = playerTransform;
        }
        
        public void Add(Transform unitTransform, int lvl)
        {
            unitTransform.transform.parent = _playerTransform;
            
            SceneUnit unit = new SceneUnit()
            {
                level = lvl,
                transform = unitTransform
            };
            
            _units.Add(unit);
            
            Reform();
        }

        public void Remove()
        {
            
        }

        private void Reform()
        {
            _units = _units.OrderByDescending(x => x.level).ToList();
            
            SetInFormation();
        }
        
        private void SetInFormation()
        {
            if (_units.Count == 1)
            {
                SetInCenter();
                return;
            }

            var orbitsAmount = Mathf.CeilToInt((_units.Count - 1.0f) / _baseOrbitUnitAmount);
            var orbitsPlaced = 0;

            while (orbitsPlaced < orbitsAmount)
            {
                SetInLayer(orbitsPlaced);
                orbitsPlaced++;
            }
        }

        private void SetInLayer(int orbitNumber)
        {
            var orbitUnitsAmount = _baseOrbitUnitAmount + orbitNumber * _orbitUnitInscreaseAmount;
            var units = _units.Skip(ComputeUnitsBeforeOrbit(orbitNumber)).Take(orbitUnitsAmount).ToList();
            
            for (int i = 0; i < units.Count; i++)
            {
                var unitPosition = ComputeUnitPosition(orbitUnitsAmount, orbitNumber, i);
                
                var unit = units[i].transform;
                unit.position = unitPosition;
            }
        }

        private int ComputeUnitsBeforeOrbit(int layerNumber)
        {
            int amount = 1;

            for (int i = 0; i < layerNumber; i++)
                amount += _baseOrbitUnitAmount + i * 4;

            return amount;
        }

        private Vector3 ComputeUnitPosition(float orbitUnitsAmount, int orbitNumber, int unitIndex)
        {
            var angleIncrement = 360.0f / orbitUnitsAmount;
            var basePosition = _playerTransform.position;
            var degreesOffset = orbitNumber * _orbitAngleOffset;
            var angle = Mathf.Deg2Rad * (angleIncrement * unitIndex + degreesOffset);

            var x = Mathf.Cos(angle);
            var z = Mathf.Sin(angle);

            return new Vector3(x, 0.0f, z)
                   * (_orbitOffset * (orbitNumber + 1.0f))
                   + basePosition;
        }

        private void SetInCenter()
        {
            var basePosition = _playerTransform.position;

            var sceneObject = _units[0].transform;
            sceneObject.position = basePosition;
        }
    }
}