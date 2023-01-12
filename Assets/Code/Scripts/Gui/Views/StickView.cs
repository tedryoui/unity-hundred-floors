using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;

namespace Code.Scripts.Gui.Views
{
    public class StickView : MonoBehaviour, IPointerDownHandler
    {
        [FormerlySerializedAs("movableStick")] 
        public GameObject movableStickRoot;
        
        public MovableOnScreenStick movableStick;
        
        public GameObject staticStick;


        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Here 2");
        }
    }
}