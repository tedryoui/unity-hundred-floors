using System;
using Code.Scripts.Gui.ViewModels;
using Code.Scripts.Gui.Views;
using UnityEngine;

namespace Code.Scripts.Services
{
    public class GuiHandler : MonoBehaviour
    {
        [SerializeField] private StickViewModel _stickViewModel;
        [SerializeField] private StartPanelViewModel _startPanelViewModel;

        public StickViewModel StickViewModel => _stickViewModel;

        public StartPanelViewModel StartPanelViewModel => _startPanelViewModel;
        
        public Action OnUpdate;

        public void EnableStaticStick() => _stickViewModel.ActivateStaticStick();
        public void EnableDynamicStick() => _stickViewModel.ActivateDynamicStick();

        public void PauseGame() => _startPanelViewModel.Pause();
        public void ResumeGame() => _startPanelViewModel.Resume();
        
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