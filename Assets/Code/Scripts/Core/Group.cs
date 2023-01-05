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
        [Header("Player`s references")]
        private Transform _playerTransform;
        [SerializeField] private Transform _groupTransform;
        private Action _unitsUpdateCallback;

        [Space] [Header("Group settings")] 
        [SerializeField] private int _unitsLimit;
        [SerializeField] private float _unitSpeed;
        private List<GroupUnit> _units = new();
        [SerializeField] private FormationService _formationService;

        public void Initialize(Transform playerTransform)
        {
            _playerTransform = playerTransform;

            _formationService.Initialize(ref _playerTransform);
        }

        public bool Add(Transform unitTransform, int lvl)
        {
            if (_units.Count >= _unitsLimit)
            {
                GroupHelpers.ReactGroupOverflow(lvl);
                return false;
            }
            else
            {
                ApplyAddition(unitTransform, lvl);
                return true;
            }
        }

        private void ApplyAddition(Transform unitTransform, int lvl)
        {
            var unit = GroupHelpers.ConstructGroupUnit(unitTransform, _unitSpeed, lvl);
            GroupHelpers.AssignGroupUnitToGroup(unit, _groupTransform, ref _unitsUpdateCallback);

            _units.Add(unit);
            Reform();
        }

        public void Remove(int lvl)
        {
            var unit = _units.FirstOrDefault(x => x.level.Equals(lvl));
            
            if (unit != null)
            {
                GroupHelpers.ProvideUnitDestruction(unit, ref _unitsUpdateCallback, _units);
                Reform();
            }
        }

        private void Reform()
        {
            _units = _units.OrderByDescending(x => x.level).ToList();
            UpdateGroup();
        }

        public void UpdateGroup()
        {
            _formationService.Form(ref _units);
            _unitsUpdateCallback?.Invoke();
        }
    }
}