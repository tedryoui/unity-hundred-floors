﻿using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Core;
using Code.Scripts.Helpers;
using Code.Scripts.Units;
using UnityEngine;

namespace Code.Scripts.Services
{
    [Serializable]
    public class GroupService
    {
        private Group _group;
        
        [SerializeField] private int _unitsLimit;
        [SerializeField] private float _unitSpeed;
        [SerializeField] private float _unitSpeedDecrease;
        private int _points;

        public List<GroupUnit> units = new();
        public Action UnitsUpdateCallback;

        public int GetPoints => _points;
        public float GetSpeedDecrease => _unitSpeedDecrease;

        public void Initialize(Group group)
        {
            _group = group;
            _group.OnChange += Sort;
        }

        public bool Add(Transform unitTransform, Unit settings)
        {
            if (units.Count >= _unitsLimit)
            {
                GroupHelpers.ReactGroupOverflow(settings);
                return false;
            }
            else
            {
                ApplyAddition(unitTransform, settings);
                _group.OnChange?.Invoke();
                return true;
            }
        }

        private void ApplyAddition(Transform unitTransform, Unit settings)
        {
            var unit = MakeGroupUnit(unitTransform, settings);

            _points += unit.unit.points;
            units.Add(unit);
            Sort();
        }

        private GroupUnit MakeGroupUnit(Transform unitTransform, Unit settings)
        {
            var unit = new GroupUnit
            {
                unit = settings,
                objectTransform = unitTransform,
                Speed = _unitSpeed - _unitSpeedDecrease
            };
            
            unit.objectTransform.parent = _group._groupTransform;
            UnitsUpdateCallback += unit.Update;
            
            return unit;
        }

        public void Remove(int points)
        {
            var unit = units.FirstOrDefault(x => x.unit.points.Equals(points));
            Remove(unit);
        }

        private void Remove(GroupUnit unit)
        {
            if (unit != null)
            {
                _points -= unit.unit.points;
                GroupHelpers.ProvideUnitDestruction(unit, ref UnitsUpdateCallback, units);
                Sort();
                _group.OnChange?.Invoke();
            }
        }
        
        private void Sort() => units = units.OrderByDescending(x => x.unit.points).ToList();

        private void CorrectUnitsSpeed()
        {
            
        }
    }
}