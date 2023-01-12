using System;
using Code.Scripts.Gui.ViewModels;
using UnityEngine;

namespace Code.Scripts.Services
{
    public class GuiHandler : MonoBehaviour
    {
        [SerializeField] private StickViewModel _stickViewModel;

        public StickViewModel StickViewModel => _stickViewModel;

        public Action OnUpdate;

        public void EnableStaticStick() => _stickViewModel.ActivateStaticStick();
        public void EnableDynamicStick() => _stickViewModel.ActivateDynamicStick();

        private void Start()
        {
            OnUpdate += _stickViewModel.UpdateStick;
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}