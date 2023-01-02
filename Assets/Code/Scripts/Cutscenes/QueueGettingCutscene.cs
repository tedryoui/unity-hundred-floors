using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Core;
using Code.Scripts.Units;
using UnityEngine;
using Queue = Code.Scripts.Core.Queue;

namespace Code.Scripts.Cutscenes
{
    public class QueueGettingCutscene : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Queue _queue;
        [SerializeField] private Camera _followCamera;
        [SerializeField] private float _delayBeforeDestroy;
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

            ReleasePlayer();
            CloseCutscene();
        }

        private void CatchPlayer()
        {
            _mainCamera = Camera.main;
            _player.gameObject.SetActive(false);
        }

        private void ReleasePlayer()
        {
            _player.Group.Add(_queue.Head.Transform, 1);
            _player.gameObject.SetActive(true);
        }

        private void CloseCutscene()
        {
            _queue.RemoveHead();
            Destroy(gameObject);
        }
    }
}