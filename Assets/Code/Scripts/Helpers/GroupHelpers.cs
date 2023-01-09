using System;
using System.Collections.Generic;
using Code.Scripts.Core;
using Code.Scripts.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Scripts.Helpers
{
    public class GroupHelpers
    {
        public static void ReactGroupOverflow(Unit settings)
        {
            
        }

        public static void InstantiateToGroup(Group group, Unit settings, Vector3 spawnPos)
        {
            GameObject unit = Object.Instantiate(settings.prefab, spawnPos, Quaternion.identity);
            group.GroupService.Add(unit.transform, settings);
        }

        public static void ProvideUnitDestruction(GroupUnit unit, ref Action unitsUpdateCallback, List<GroupUnit> units)
        {
            Object.Destroy(unit.objectTransform.gameObject);

            unitsUpdateCallback -= unit.Update;
            units.Remove(unit);
        }
    }
}