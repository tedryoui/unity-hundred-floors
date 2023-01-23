using System;
using Code.Scripts.Gui.ViewModels;
using UnityEngine;

namespace Code.Scripts.Gui
{
    public class GuiHandler : MonoBehaviour
    {
        [SerializeField] private StickViewModel _stickViewModel;
        [SerializeField] private StartPanelViewModel _startPanelViewModel;

        public StickViewModel StickViewModel => _stickViewModel;

        public StartPanelViewModel StartPanelViewModel => _startPanelViewModel;
        
        public Action OnUpdate;

        public void ResumeGame() => _startPanelViewModel.Resume();

        public void EnableStaticStick(bool state) => _stickViewModel.EnableStaticStick(state);
        public void EnableDynamicStick(bool state) => _stickViewModel.ActivateDynamicStick(state);
        
        private void Start()
        {
            EnableStaticStick(true);
            EnableDynamicStick(false);
        }

        private void Update()
        {
        }
    }
}