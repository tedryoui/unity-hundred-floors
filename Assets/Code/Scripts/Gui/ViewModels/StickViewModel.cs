using System;
using Code.Scripts.Core;
using Code.Scripts.Gui.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

namespace Code.Scripts.Gui.ViewModels
{
    [Serializable]
    public class StickViewModel
    {
        enum ActivatedStick
        {
            Static, Dynamic
        };
        
        [SerializeField] private StickView _view;

        private ActivatedStick _stick = ActivatedStick.Static;
        
        public void EnableStaticStick(bool state)
        {
            _view.staticStickImage.enabled = state;
            _view.staticBackgroundImage.enabled = state;
        }

        public void ActivateDynamicStick(bool state)
        {
            _view.dynamicRaycastAreaImage.enabled = state;
            _view.dynamicStickImage.enabled = state;
            _view.dynamicBackgroundImage.enabled = state;
        }
    }
}