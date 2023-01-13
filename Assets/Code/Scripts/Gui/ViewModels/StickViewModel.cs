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

        private ActivatedStick _stick = ActivatedStick.Dynamic;
        private InputControls _controls => GameCore.GetInput;
        
        public void ActivateStaticStick()
        {
            _view.movableStickRoot.SetActive(false);
            _view.staticStick.SetActive(true);
            _stick = ActivatedStick.Static;
        }

        public void ActivateDynamicStick()
        {
            _view.movableStickRoot.SetActive(true);
            _view.staticStick.SetActive(false);
            _stick = ActivatedStick.Dynamic;
        }

        public void UpdateStick()
        {
            
        }

        public Vector2 GetStickDirection()
        {
            var direction = _controls.Stick.Direction.ReadValue<Vector2>();
            return direction;
        }
    }
}