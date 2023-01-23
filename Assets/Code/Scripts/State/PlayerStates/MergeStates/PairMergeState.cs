using System.Collections.Generic;
using Code.Scripts.Core;
using Code.Scripts.Core.Player;
using Code.Scripts.Units;
using UnityEngine;

namespace Code.Scripts.State.PlayerStates.MergeStates
{
    public class PairMergeState : MergeState
    {
        private int Interations => Mathf.FloorToInt(Units.Count / 2.0f);
        private Vector3 GroupPosition => Group._groupPosition.position;
        
        public PairMergeState(Player actor) : base(actor)
        {
        }

        protected override void TryMerge(Unit unitsPreset, List<GroupUnit> groupUnits)
        {
            for (int i = 0; i < Interations; i++)
            {
                // Removing two same units
                Group.Remove(unitsPreset.points);
                Group.Remove(unitsPreset.points);

                // Checking if the next tier unit exists
                if (ReferenceEquals(unitsPreset.next, null))
                {
                    // TODO - React to OverGrade
                }
                else
                {
                    // Merging and grading up units
                    Merge(unitsPreset);
                }
            }
        }

        private void Merge(Unit unitsPreset)
        {
            // Getting free entity from the pool
            GameObject obj = GameCore.GetLevel.GetPool.GetEntity(unitsPreset.next.prefab.name);

            // If poll has such entity, then prepare it and add to the actor`s group
            if (!ReferenceEquals(obj, null))
            {
                obj.transform.position = GroupPosition;
                Group.TryAdd(obj.transform, unitsPreset.next);
            }
        }
    }
}