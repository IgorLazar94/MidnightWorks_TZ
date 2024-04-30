using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RacingDriftGame.Scripts.UI
{
    public class HUDButton : MonoBehaviour
    {
        public bool IsPressed { get; private set; }

        public float dampenPress { get; private set; }
        private float sensitivity = 1f;

        private void Start()
        {
            dampenPress = 0;
            IsPressed = false;
            SetUpButton();
        }

        private void Update()
        {
            if (IsPressed)
            {
                dampenPress += sensitivity * Time.deltaTime;
            }
            else
            {
                dampenPress -= sensitivity * Time.deltaTime;
            }

            dampenPress = Mathf.Clamp01(dampenPress);
        }

        private void SetUpButton()
        {
            var eventTrigger = gameObject.AddComponent<EventTrigger>();
            var pointerDown = new EventTrigger.Entry();
            pointerDown.eventID = EventTriggerType.PointerDown;
            pointerDown.callback.AddListener((e) => OnCLickDown());

            var pointerUp = new EventTrigger.Entry();
            pointerUp.eventID = EventTriggerType.PointerUp;
            pointerUp.callback.AddListener((e) => OnCLickUp());
            
            eventTrigger.triggers.Add(pointerDown);
            eventTrigger.triggers.Add(pointerUp);
        }

        public void OnCLickDown()
        {
            IsPressed = true;
        }
        
        public void OnCLickUp()
        {
            IsPressed = false;
        }
        
        
        
    }
}
