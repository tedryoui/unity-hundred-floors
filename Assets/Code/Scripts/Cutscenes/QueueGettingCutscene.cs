using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Core;
using Code.Scripts.Units;
using UnityEngine;
using UnityEngine.Events;
using Queue = Code.Scripts.Core.Queue;

namespace Code.Scripts.Cutscenes
{
    public class QueueGettingCutscene : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Queue _queue;
        [SerializeField] private Camera _followCamera;
        [SerializeField] private float _delayBeforeDestroy;
        [SerializeField] private UnityEvent _onCutsceneCloseEvent;
        private Camera _mainCamera;

        private void Start()
        {
            CatchPlayer();
            
            StartCutscene();
        }

        private void StartCutscene()
        {
            StartCoroutine(_queue.MoveTheQueue());
            _queue.MoveTheQueueHead();
            StartCoroutine(FollowQueueHead());
        }

        private IEnumerator FollowQueueHead()
        {
            Camera.SetupCurrent(_followCamera);
            _followCamera.targetDisplay = 0;
            
            yield return new WaitWhile(() => _queue.Head.Status.Equals(QueueUnit.QueueUnitStatus.Move));
            Camera.SetupCurrent(_mainCamera);
            yield return new WaitForSeconds(_delayBeforeDestroy);
            
            CloseCutscene();
        }

        private void CatchPlayer()
        {
            _mainCamera = Camera.main;
            _player.gameObject.SetActive(false);
        }

        private void CloseCutscene()
        {
            _onCutsceneCloseEvent?.Invoke();

            Destroy(gameObject);
        }
    }
}