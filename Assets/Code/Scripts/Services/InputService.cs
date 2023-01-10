using System;
using Code.Scripts.Core;
using UnityEngine;

namespace Code.Scripts.Services
{
    [Serializable]
    public class InputService
    {
        private Player _player;
        private InputControls _controls;

        public InputControls Controls => _controls;
        
        public void Initialize(Player player)
        {
            _player = player;
            
            _controls = new InputControls();
        }

        public void Update()
        {
            ProcessMoveInput();
            ProcessMergeInput();
        }

        private void ProcessMoveInput()
        {
            Vector2 inputDirection = _controls.Player.Stick.ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(inputDirection.x, 0.0f, inputDirection.y);
            
            _player.transform.position += moveDirection * (_player.speed * Time.deltaTime);
        }

        private void ProcessMergeInput()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                _player.Group.Merge();
        }
    }
}