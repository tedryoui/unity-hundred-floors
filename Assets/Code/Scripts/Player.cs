using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private UnitGroup _group;

        public Unit lvl1Unit;
        public Unit lvl2Unit;
        public Unit lvl3Unit;
        
        private void Awake()
        {
            _group.Initialize(this.transform);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                GameObject unit = Instantiate(lvl1Unit.prefab, Vector3.zero, Quaternion.identity);
                _group.Add(unit.transform, lvl1Unit.level);
            } else if (Input.GetKeyDown(KeyCode.X))
            {
                GameObject unit = Instantiate(lvl2Unit.prefab, Vector3.zero, Quaternion.identity);
                _group.Add(unit.transform, lvl2Unit.level);
            } else if (Input.GetKeyDown(KeyCode.C))
            {
                GameObject unit = Instantiate(lvl3Unit.prefab, Vector3.zero, Quaternion.identity);
                _group.Add(unit.transform, lvl3Unit.level);
            }
        }
    }
}