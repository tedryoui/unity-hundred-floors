using System;
using Code.Scripts.Units;
using UnityEngine;

namespace Code.Scripts.Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Group _group;
        [SerializeField] private Action _playerUpdateCallback;

        public Unit lvl1Unit;
        public Unit lvl2Unit;
        public Unit lvl3Unit;

        private void Awake()
        {
            _group.Initialize(transform);
            _playerUpdateCallback += _group.UnitsUpdateCallback;
        }

        private void Update()
        {
            TestInput();

            _group.UnitsUpdateCallback?.Invoke();
        }

        private void TestInput()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                var unit = Instantiate(lvl1Unit.prefab, Vector3.zero, Quaternion.identity);
                _group.Add(unit.transform, lvl1Unit.level);
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                var unit = Instantiate(lvl2Unit.prefab, Vector3.zero, Quaternion.identity);
                _group.Add(unit.transform, lvl2Unit.level);
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                var unit = Instantiate(lvl3Unit.prefab, Vector3.zero, Quaternion.identity);
                _group.Add(unit.transform, lvl3Unit.level);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                _group.Remove(lvl1Unit.level);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _group.Remove(lvl2Unit.level);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _group.Remove(lvl3Unit.level);
            }
        }
    }
}