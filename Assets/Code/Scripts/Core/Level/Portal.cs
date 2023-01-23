using System;
using System.Linq;
using Code.Scripts.Core.Player;
using Code.Scripts.Services;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Code.Scripts.Core
{
    [Serializable]
    public class Portal : MonoBehaviour, IInteractable
    {
        public Action OnPlayerEntered;

        public void Catch()
        {
            gameObject.SetActive(false);
        }

        public void Release()
        {
            gameObject.SetActive(true);
        }

        public void Accept(IInteractor interactor)
        {
            if (interactor is PlayerInteractor)
            {
                GameCore.GetPlayer.ChangeState(GameCore.GetPlayer.MergeState);
                OnPlayerEntered?.Invoke();
            }
        }
    }
}