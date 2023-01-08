﻿using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Core;
using Code.Scripts.Units;
using UnityEngine;

namespace Code.Scripts.Services
{
    [Serializable]
    public class GroupService
    {
        private Transform _groupTransform;
        
        [SerializeField] private int _unitsLimit;
        [SerializeField] private float _unitSpeed;
        private int _points;

        public List<GroupUnit> units = new();
        public Action UnitsUpdateCallback;

        public int GetPoints => _points;

        public void Initialize(Group group)
        {
            _groupTransform = group._groupTransform;

            group.OnChange += Sort;
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
                return true;
            }
        }

        private void ApplyAddition(Transform unitTransform, Unit settings)
        {
            var unit = MakeGroupUnit(unitTransform, settings);

            _points += unit.settings.points;
            units.Add(unit);
            Sort();
        }

        private GroupUnit MakeGroupUnit(Transform unitTransform, Unit settings)
        {
            var unit = new GroupUnit
            {
                settings = settings,
                objectTransform = unitTransform,
                speed = _unitSpeed
            };
            
            unit.objectTransform.parent = _groupTransform;
            UnitsUpdateCallback += unit.Update;
            
            return unit;
        }

        public void Remove(int points)
        {
            var unit = units.FirstOrDefault(x => x.settings.points.Equals(points));
            Remove(unit);
        }

        private void Remove(GroupUnit unit)
        {
            if (unit != null)
            {
                _points -= unit.settings.points;
                GroupHelpers.ProvideUnitDestruction(unit, ref UnitsUpdateCallback, units);
                Sort();
            }
        }
        
        private void Sort() => units = units.OrderByDescending(x => x.settings.points).ToList();
    }
}