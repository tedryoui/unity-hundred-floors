using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Core;
using Code.Scripts.Helpers;
using Code.Scripts.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Scripts.Services
{
    [Serializable]
    public class MergeService
    {
        private Group _group;

        private Vector3 GetPlayerPosition => GameCore.GetPlayer.transform.position;
        
        public void Initialize(Group group)
        {
            _group = group;
        }

        public void Merge(List<GroupUnit> units)
        {
            var levelGroups = 
                units.GroupBy(x => x.unit)
                     .Select(x => new {x.Key, GroupUnits = x.ToList<GroupUnit>()})
                     .OrderBy(x => x.Key);
            
            foreach (var group in levelGroups)
            {
                var unitType = group.Key;
                var groupUnits = group.GroupUnits;
                MergeGroup(unitType, groupUnits);
            }
        }

        private void MergeGroup(Unit identifier, List<GroupUnit> units)
        {
            var mergeCount = Mathf.FloorToInt(units.Count / 2.0f);
            
            for(int i = 0; i < mergeCount; i++)
                MergeTwo(units, identifier);
        }

        private void MergeTwo(List<GroupUnit> units, Unit identifier)
        {
            var spawnPos = GetPlayerPosition;
            
            _group.GroupService.Remove(identifier.points);
            _group.GroupService.Remove(identifier.points);
            
            if (ReferenceEquals(identifier.next, null))
                ReactToOverGrade(identifier, units.Count);
            else
                GroupHelpers.InstantiateToGroup(_group, identifier.next, spawnPos);
        }

        private void ReactToOverGrade(Unit identifier, int unitsCount)
        {
            
        }
    }
}