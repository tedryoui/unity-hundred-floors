using System;
using UnityEngine;

namespace Code.Scripts.Group.Formation
{
    [Serializable]
    [CreateAssetMenu(menuName = "Formation/Dot formation")]
    public class DotFormation : Core.Formation
    {
        public override int FormationSize => 1;
        
        //
        //
        //      0 <- - - All units in the same position
        //
        //
        
        protected override void Form()
        {
            // Looping through each unit
            foreach (var unit in UnitsReference)
            {
                // Set unit to follow the central formation position
                unit.targetPosition = FormationPosition;
            }
        }
    }
}