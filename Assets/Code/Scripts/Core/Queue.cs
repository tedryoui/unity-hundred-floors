using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Services;
using Code.Scripts.Units;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

namespace Code.Scripts.Core
{
    public class Queue : MonoBehaviour
    {
        [Header("Scene references")]
        [SerializeField] private Player _player;
        [SerializeField] private Transform _unitsGroup;
        
        [Space]
        [Header("Queue settings")]
        [SerializeField] private Transform _queueStartPosition;
        [SerializeField] private Vector3 _queueDirection;
        [SerializeField] private float _unitToUnitOffset;
        [SerializeField] private int _unitsAmount;
        [SerializeField] private float _delayBetweenOffsets;

        [Space] [Header("Queue units settings")] 
        [SerializeField] private Unit _unitPrefab;
        [SerializeField] private List<AnimationCurve> _speedInterpolations;
        [SerializeField] private float _unitSpeed;

        private List<QueueUnit> _units = new List<QueueUnit>();
        private Action _unitsUpdateCallback;

        private AnimationCurve SpeedInterpolation => _speedInterpolations[Random.Range(0, _speedInterpolations.Count - 1)];
        public QueueUnit Head => _units[0];
        
        private void Awake()
        {
            for (int i = 0; i < _unitsAmount; i++)
            {
                Vector3 pos = _queueStartPosition.position + _queueDirection * i * _unitToUnitOffset;
                GameObject obj = GameObject.Instantiate(_unitPrefab.prefab, pos, Quaternion.identity, _unitsGroup);

                QueueUnit unit = new QueueUnit()
                {
                    Transform = obj.transform,
                    Placement = pos,
                    Status = QueueUnit.QueueUnitStatus.Wait,
                    SpeedCurve = SpeedInterpolation,
                    Speed = _unitSpeed
                };

                _unitsUpdateCallback += unit.Update;
                _units.Add(unit);
            }
        }
        
        public void RemoveHead()
        {
            _units.RemoveAt(0);
        }

        public void MoveTheQueueHead()
        {
            _units[0].Placement = _player.transform.position;
            _units[0].Speed = _unitSpeed * 2.0f;
            _units[0].Status = QueueUnit.QueueUnitStatus.Move;
        }
        
        public IEnumerator MoveTheQueue()
        {
            for (int i = _units.Count - 1; i > 0; i--)
                _units[i].Placement = _units[i - 1].Placement;

            foreach (var unit in _units)
            {
                unit.Status = QueueUnit.QueueUnitStatus.Move;
                yield return new WaitForSeconds(_delayBetweenOffsets);
            }
        }

        public void SetFirstUnit()
        {
            _player.gameObject.SetActive(true);
            _player.Group.GroupService.Add(Head.Transform, _unitPrefab);
            
            RemoveHead();
        }

        private void Update()
        {
            _unitsUpdateCallback?.Invoke();
        }
    }
}