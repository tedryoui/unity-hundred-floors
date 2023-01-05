using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Scripts.Services
{
    public class GroupHelpers
    {
        public static void ReactGroupOverflow(int lvl)
        {
            Debug.Log($"Added {lvl} coins to player wallet!");
        }

        public static GroupUnit ConstructGroupUnit(Transform unitTransform, float unitSpeed, int lvl)
        {

            var unit = new GroupUnit
            {
                level = lvl,
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
    }
}