using System;
using System.Collections.Generic;
using Code.Scripts.Units;
using UnityEngine;

namespace Code.Scripts.Core
{
    [Serializable]
    public abstract class Formation : ScriptableObject
    {
        protected Vector3 FormationPosition;
        protected List<GroupUnit> UnitsReference;
        
        public abstract int FormationSize { get; }
        
        public void Form(Group group)
        {
            UnitsReference = group.Units;
            FormationPosition = group._groupPosition.position;
            
            Form();
        }

        protected abstract void Form();
    }
}