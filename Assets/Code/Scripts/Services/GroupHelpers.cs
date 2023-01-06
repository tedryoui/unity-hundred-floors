using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Core;
using Code.Scripts.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Scripts.Services
{
    public class GroupHelpers
    {
        public static void ReactGroupOverflow(Unit settings)
        {
            Debug.Log($"Added {settings.order} coins to player wallet!");
        }

        public static void InstantiateToGroup(Unit settings, Vector3 spawnPos, Group group)
        {
            GameObject unit = Object.Instantiate(settings.prefab, spawnPos, Quaternion.identity);
            group.Add(unit.transform, settings);
        }

        public static GroupUnit ConstructGroupUnit(Transform unitTransform, float unitSpeed, Unit settings)
        {

            var unit = new GroupUnit
            {
                settings = settings,
                objectTransform = unitTransform,
                speed = unitSpeed
            };
            
            return unit;
        }

        public static void AssignGroupUnitToGroup(GroupUnit unit, Transform unitParent, ref Action unitsUpdateCallback)
        {
            unit.objectTransform.parent = unitParent;
            unitsUpdateCallback += unit.Update;
        }

        public static void ProvideUnitDestruction(GroupUnit unit, ref Action unitsUpdateCallback, List<GroupUnit> units)
        {
            Object.Destroy(unit.objectTransform.gameObject);

            unitsUpdateCallback -= unit.Update;
            units.Remove(unit);
        }

        public static void ReactUnitOvergrade(Unit identifier, int unitsCount)
        {
            Debug.Log($"Added {identifier.order} coins to player wallet!");
        }
    }
}