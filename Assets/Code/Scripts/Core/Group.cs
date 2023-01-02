using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Scripts.Core
{
    [Serializable]
    public class Group
    {
        [Header("Player`s references")]
        private Transform _playerTransform;
        [SerializeField] private Transform _groupTransform;
        public Action UnitsUpdateCallback;

        [Space]
        [Header("Group settings")]
        [SerializeField] private List<SceneUnit> _units = new();
        [SerializeField] private CircleFormationService _formationService;

        public void Initialize(Transform playerTransform)
        {
            _playerTransform = playerTransform;

            _formationService.Initialize(
                ref _playerTransform
            );
        }

        public void Add(Transform unitTransform, int lvl)
        {
            unitTransform.transform.parent = _groupTransform;

            var unit = new SceneUnit
            {
                level = lvl,
                transform = unitTransform
            };
            UnitsUpdateCallback += unit.Update;

            _units.Add(unit);
            Reform();
        }

        public void Remove(int lvl)
        {
            var unit = _units.FirstOrDefault(x => x.level.Equals(lvl));

            if (unit != null)
            {
                Object.Destroy(unit.transform.gameObject);

                UnitsUpdateCallback -= unit.Update;
                _units.Remove(unit);

                Reform();
            }
        }

        private void Reform()
        {
            _units = _units.OrderByDescending(x => x.level).ToList();

            _formationService.Form(ref _units);
        }
    }
}