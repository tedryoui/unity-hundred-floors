using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Core;
using Code.Scripts.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Scripts.Services
{
    [Serializable]
    public class MergeService
    {
        private Group _group;
        
        public void Initialize(Group group)
        {
            _group = group;
        }

        public void Merge(List<GroupUnit> units)
        {
            var levelGroups = units.GroupBy(x => x.settings)
                .Select(x => new {
                    x.Key, GroupUnits = x.ToList<GroupUnit>()
                });
            
            foreach (var group in levelGroups)
            {
                var identifier = group.Key;
                var groupUnits = group.GroupUnits;
                MergeGroup(identifier, groupUnits);
            }
        }

        private void MergeGroup(Unit identifier, List<GroupUnit> units)
        {
            var nextLevel = identifier.nextLevel;
            var leftIterator = 0;
            var rightIterator = units.Count - 1;

            while (leftIterator < rightIterator)
            {
                MergeTwo(units, identifier, leftIterator, nextLevel);

                leftIterator++;
                rightIterator--;
            }
        }

        private void MergeTwo(List<GroupUnit> units, Unit identifier, int leftIterator, Unit nextLevel)
        {
            Vector3 spawnPos = units[leftIterator].TargetPosition;

            if (ReferenceEquals(nextLevel, null))
            {
                ReactToOverGrade(identifier, units.Count);
            }
            else
            {
                GroupHelpers.InstantiateToGroup(_group, nextLevel, spawnPos);
            }
                
            _group.GroupService.Remove(identifier.points);
            _group.GroupService.Remove(identifier.points);
        }

        private void ReactToOverGrade(Unit identifier, int unitsCount)
        {
            
        }
    }
}