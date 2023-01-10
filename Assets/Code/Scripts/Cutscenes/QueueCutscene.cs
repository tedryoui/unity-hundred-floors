using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Code.Scripts.Core;
using Code.Scripts.Units;
using UnityEngine;
using UnityEngine.Events;
using Queue = Code.Scripts.Core.Queue;

namespace Code.Scripts.Cutscenes
{
    public class QueueCutscene : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Queue _queue;
        [SerializeField] private CinemachineVirtualCamera _vCam;
        [SerializeField] private float _delayBeforeDestroy;
        [SerializeField] private UnityEvent _onCutsceneCloseEvent;

        private void Start()
        {
            CatchPlayer();
            SetupCamera();
            
            StartCutscene();
        }

        private void SetupCamera()
        {
            _vCam.LookAt = _queue.Head.Transform;
        }

        private void StartCutscene()
        {
            StartCoroutine(_queue.MoveTheQueue());
            _queue.MoveTheQueueHead();
            StartCoroutine(FollowQueueHead());
        }

        private IEnumerator FollowQueueHead()
        {
            _vCam.Priority = 100;
            
            yield return new WaitWhile(() => _queue.Head.Status.Equals(QueueUnit.QueueUnitStatus.Move));
            yield return new WaitForSeconds(_delayBeforeDestroy);
            
            CloseCutscene();
        }

        private void CatchPlayer() => _player.gameObject.SetActive(false);

        private void CloseCutscene()
        {
            _vCam.Priority = 0;
            _onCutsceneCloseEvent?.Invoke();

            StartCoroutine(DestroyOnCameraChanged());
        }

        private IEnumerator DestroyOnCameraChanged()
        {
            yield return new WaitForSecondsRealtime(10.0f);
            Destroy(gameObject);
        }
    }
}