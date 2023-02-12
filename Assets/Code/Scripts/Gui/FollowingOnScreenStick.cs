using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;

// Referenced OnScreenStick class

namespace Code.Scripts.Gui
{
    [AddComponentMenu("Input/On-Screen Following Stick")]
    public class FollowingOnScreenStick : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {
        private void CorrectPosition(Vector2 delta)
        {
            if (delta.magnitude / movementRange >= 0.90f)
            {
                var moveVector = Vector2.MoveTowards(m_PointerDownPos, m_PointerDownPos + delta, delta.magnitude / m_FollowSpeed);
                m_PointerDownPos = moveVector;
            }
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));

            m_CachedEventData = eventData;
            m_IsPointerUp = false;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out m_PointerDownPos);
            
            m_StickRoot.gameObject.SetActive(true);
            m_StickBackground.gameObject.SetActive(true);
            m_StickBackground.position = m_PointerDownPos;
            m_StickRoot.position = m_PointerDownPos;
        }

        private void Update()
        {
            if (!m_IsPointerUp)
            {
                if (m_CachedEventData == null)
                    throw new System.ArgumentNullException(nameof(m_CachedEventData));

                RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponentInParent<RectTransform>(), m_CachedEventData.position, m_CachedEventData.pressEventCamera, out var position);
                var delta = position - m_PointerDownPos;
                
                CorrectPosition(delta);

                delta = Vector2.ClampMagnitude(delta, movementRange);
                m_StickRoot.position = m_PointerDownPos + delta;
                m_StickBackground.position = m_PointerDownPos;

                var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);
                SendValueToControl(newPos);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_CachedEventData = null;
            m_IsPointerUp = true;
            
            m_StickRoot.gameObject.SetActive(false);
            m_StickBackground.gameObject.SetActive(false);
            SendValueToControl(Vector2.zero);
        }

        public float movementRange
        {
            get => m_MovementRange;
            set => m_MovementRange = value;
        }

        [SerializeField] 
        private Transform m_StickRoot;

        [SerializeField] 
        private Transform m_StickBackground;
        
        [SerializeField]
        private float m_MovementRange = 50;

        [SerializeField] 
        private float m_FollowSpeed = 1.0f;

        [InputControl(layout = "Vector2")]
        [SerializeField]
        private string m_ControlPath;

        private Vector2 m_PointerDownPos;
        private bool m_IsPointerUp = true;
        private PointerEventData m_CachedEventData;

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }
    }
}
