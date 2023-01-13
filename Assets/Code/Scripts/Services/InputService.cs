using System;
using Code.Scripts.Core;
using UnityEngine;

namespace Code.Scripts.Services
{
    [Serializable]
    public class InputService
    {
        private Player _player;
        
        private Vector3 _moveDirection;

        public Vector3 GetMoveDirection
        {
            get
            {
                return _moveDirection;
            }
        }
        
        public void Initialize(Player player)
        {
            _player = player;
        }

        public void Update()
        {
            UpdateMoveDirection();
        }

        private void UpdateMoveDirection()
        {
            Vector2 inputDirection = GameCore.GetGuiHandler.StickViewModel.GetStickDirection();
            _moveDirection = new Vector3(inputDirection.x, 0.0f, inputDirection.y);
            
            Debug.Log(_moveDirection);

            _player.transform.position += _moveDirection * (_player.speed * Time.deltaTime);
        }
    }
}