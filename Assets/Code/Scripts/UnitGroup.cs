using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Scripts
{
    [Serializable]
    public class UnitGroup
    {
        // Add one
        // Add few
        // Remove one
        // Remove few
        // Merge
        [SerializeField] private List<Unit> _units = new();
        [SerializeField] private int _baseOrbitUnitAmount;
        //[SerializeField] private Formation formation;
        
        public void Add(Transform sceneObj, int lvl)
        {
            Unit unit = new Unit()
            {
                Level = lvl,
                SceneObject = sceneObj
            };
            
            _units.Add(unit);
            
            Reform();
        }

        public void Remove()
        {
            
        }

        private void Reform()
        {
            _units = _units.OrderBy(x => x.Level).ToList();
            
            SetInFormation();
        }
        
        private void SetInFormation()
        {
            var layerNumber = 1;
            var settedAmount = 0;

            while (settedAmount != _units.Count)
            {
                if (++settedAmount == 0) SetInCenter();
                else SetInLayer(layerNumber);
            }

        }

        private void SetInLayer(int layerNumber)
        {
            // Take player position
            var basePosition = new Vector3(0, 1, 0);
            var orbitUnitsAmount = _baseOrbitUnitAmount;
            var baseDirection = Vector3.forward;
            var angleIncrement = 360.0f / orbitUnitsAmount;

            for (int i = 0; i < orbitUnitsAmount; i++)
            {
                var angle = (angleIncrement * Mathf.PI / 360.0f) * i;
                
                Debug.Log($"Current angle: {angle}");
            }
        }

        private void SetInCenter()
        {
            // Take player position
            var basePosition = new Vector3(0, 1, 0);

            var sceneObject = _units[0].SceneObject;
            sceneObject.position = basePosition;
        }
    }
}