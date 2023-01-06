using System;
using Code.Scripts.Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Group _group;

        public Unit lvl1Unit;
        public Unit lvl2Unit;
        public Unit lvl3Unit;
        public Group Group => _group;

        private void Awake()
        {
            _group.Initialize(transform);
            
            //TestUnits();
        }

        private void Update()
        {
            TestInput();
            
            _group.UpdateGroup();
        }

        private void TestUnits()
        {
            var amount = Random.Range(1, 10);

            for (int i = 0; i < amount; i++)
            {
                GameObject unit;
                Vector3 pos = new(Random.Range(0.0f, 10.0f), 0.0f, Random.Range(0.0f, 10.0f));
                unit = Instantiate(lvl1Unit.prefab, pos, Quaternion.identity);
                
                bool result = _group.Add(unit.transform, lvl1Unit);
                if(!result)
                    Destroy(unit);
            }
        }

        private void TestInput()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                _group.Merge();
            }
        }
    }
}