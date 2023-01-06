using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Services;
using Code.Scripts.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Scripts.Core
{
    [Serializable]
    public class Group
    {
        private Transform _playerTransform;
        [SerializeField] private Transform _groupTransform;
        private Action _unitsUpdateCallback;

        [Space] [Header("Group settings")] 
        [SerializeField] private int _unitsLimit;
        [SerializeField] private float _unitSpeed;
        private List<GroupUnit> _units = new();
        [SerializeField] private FormationService _formationService;
        [SerializeField] private MergeService _mergeService;

        public Action OnChange;
        public float OrbitOffset => _formationService.OrbitOffset;
        public int OrbitsAmount => _formationService.OrbitsAmount;

        public void Initialize(Transform playerTransform)
        {
            _playerTransform = playerTransform;

            _formationService.Initialize(ref _playerTransform);
            _mergeService.Initialize(this);
        }

        public bool Add(Transform unitTransform, Unit settings)
        {
            if (_units.Count >= _unitsLimit)
            {
                GroupHelpers.ReactGroupOverflow(settings);
                return false;
            }
            else
            {
                ApplyAddition(unitTransform, settings);
                return true;
            }
        }

        private void ApplyAddition(Transform unitTransform, Unit settings)
        {
            var unit = GroupHelpers.ConstructGroupUnit(unitTransform, _unitSpeed, settings);
            GroupHelpers.AssignGroupUnitToGroup(unit, _groupTransform, ref _unitsUpdateCallback);

            _units.Add(unit);

            Reform();
        }

        public void Remove(int order)
        {
            var unit = _units.FirstOrDefault(x => x.settings.order.Equals(order));
            
            if (unit != null)
            {
                GroupHelpers.ProvideUnitDestruction(unit, ref _unitsUpdateCallback, _units);
                Reform();
            }
        }

        public void RemoveAt(int at)
        {
            var unit = _units[at];
            
            if (unit != null)
            {
                GroupHelpers.ProvideUnitDestruction(unit, ref _unitsUpdateCallback, _units);
                Reform();
            }
        }

        public void Merge()
        {
            _mergeService.Merge(_units);
            
            OnChange?.Invoke();
        }
        
        private void Reform()
        {
            _units = _units.OrderBy(x => x.settings.order).ToList();
            UpdateGroup();
            OnChange?.Invoke();
        }

        public void UpdateGroup()
        {
            _formationService.Form(ref _units);
            _unitsUpdateCallback?.Invoke();
        }
    }
}