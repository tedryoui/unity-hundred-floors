using System;
using Cinemachine;
using UnityEngine;

namespace Code.Scripts.Core.Player
{
    [Serializable]
    public class PlayerCameraService
    {
        private Player _player;
        
        [SerializeField] private CinemachineVirtualCamera _vCam;
        [SerializeField] private float _groupRadiusDelemeter;
        [SerializeField] private float _baseVerticalArmLength;
        [SerializeField] private float _baseCameraDistance;

        private Cinemachine3rdPersonFollow _followComponent;

        public Cinemachine3rdPersonFollow FollowComponent
        {
            get
            {
                if (ReferenceEquals(_followComponent, null))
                    _followComponent = _vCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

                return _followComponent;
            }
        }
        
        public void Initialize(Player player)
        {
            _player = player;
        }
        
        public void Update()
        {
            UpdateCameraFollow();
        }

        private void UpdateCameraFollow()
        {
            var targetVerticalArmLength = GetActualVerticalArmLength();
            var verticalArmLengthDiff = targetVerticalArmLength - FollowComponent.VerticalArmLength;

            var targetCameraDistance = GetActualCameraDistance();
            var cameraDistanceDiff = targetCameraDistance - FollowComponent.CameraDistance;

            FollowComponent.VerticalArmLength += verticalArmLengthDiff * Time.deltaTime;
            FollowComponent.CameraDistance += cameraDistanceDiff * Time.deltaTime;
        }

        private float GetActualCameraDistance()
        {
            var radius = _player.Group.GroupSize;

            return _baseCameraDistance * radius;
        }

        private float GetActualVerticalArmLength()
        {
            var radius = _player.Group.GroupSize;

            return _baseVerticalArmLength * radius;
        }
    }
}