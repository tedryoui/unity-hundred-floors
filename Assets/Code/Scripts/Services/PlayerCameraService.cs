using System;
using Cinemachine;
using Code.Scripts.Core;
using UnityEngine;

namespace Code.Scripts.Services
{
    [Serializable]
    public class PlayerCameraService
    {
        private Player _player;
        
        [SerializeField] private CinemachineVirtualCamera _vCam;
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
            var orbitsAmount = _player.Group.FormationService.OrbitsAmount + 1;

            return _baseCameraDistance * orbitsAmount;
        }

        private float GetActualVerticalArmLength()
        {
            var orbitsAmount = _player.Group.FormationService.OrbitsAmount + 1;

            return _baseVerticalArmLength * orbitsAmount;
        }
    }
}