using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Code.Scripts.Gui.Views
{
    public class StartPanelView : MonoBehaviour, IPointerClickHandler
    {
        public UnityEvent onClick;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke();
        }
    }
}