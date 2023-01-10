using System;
using Code.Scripts.Core;
using UnityEngine;

namespace Code.Scripts.Services
{
    [Serializable]
    public class InputService
    {
        private Player _player;
        
        public void Initialize(Player player)
        {
            _player = player;
        }

        public void Update()
        {
            ProcessMoveInput();
            ProcessMergeInput();
        }

        private void ProcessMoveInput()
        {
            var moveDir = new Vector3(
                Input.GetAxis("Horizontal"),
                0.0f,
                Input.GetAxis("Vertical")
            );

            _player.transform.position += moveDir * (_player.speed * Time.deltaTime);
        }

        private void ProcessMergeInput()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                _player.Group.Merge();
        }
    }
}