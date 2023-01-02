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
        }

        private void Update()
        {
            TestInput();

            _group.UnitsUpdateCallback?.Invoke();
        }

        private void TestInput()
        {
            if(Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                int action = Random.Range(0, 5);
                GameObject unit;

                switch (action)
                {
                    case 0:
                        unit = Instantiate(lvl1Unit.prefab, Vector3.zero, Quaternion.identity);
                        _group.Add(unit.transform, lvl1Unit.level);
                        break;
                    case 1:
                        unit = Instantiate(lvl2Unit.prefab, Vector3.zero, Quaternion.identity);
                        _group.Add(unit.transform, lvl2Unit.level);
                        break;
                    case 2:
                        unit = Instantiate(lvl3Unit.prefab, Vector3.zero, Quaternion.identity);
                        _group.Add(unit.transform, lvl3Unit.level);
                        break;
                    case 3:
                        _group.Remove(lvl1Unit.level);
                        break;
                    case 4:
                        _group.Remove(lvl2Unit.level);
                        break;
                    case 5:
                        _group.Remove(lvl3Unit.level);
                        break;
                }
            }
        }
    }
}