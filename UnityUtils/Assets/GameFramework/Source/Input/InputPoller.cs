using System;
using UnityEngine;

namespace GameFramework
{
    public sealed class InputPoller : MonoBehaviour
    {
        private InputReceiver _receiver;
        public InputReceiver Receiver
        {
            get => _receiver;
            set
            {
                if(_receiver != value)
                {
                    _receiver = value;
                }
                
            }
        }
        
        private void FixedUpdate()
        {
            if (Receiver == null)
            {
                return;
            }
            
            foreach (InputId inputId in Enum.GetValues(typeof(InputId)))
            {
                PollButton(inputId, InputEventPollingType.FixedUpdate);
                PollAxis(inputId, InputEventPollingType.FixedUpdate);
            }   
        }

        private void Update()
        {
            if (Receiver == null)
            {
                return;
            }

            foreach (InputId inputId in Enum.GetValues(typeof(InputId)))
            {
                PollButton(inputId, InputEventPollingType.Update);
                PollAxis(inputId, InputEventPollingType.Update);
            }
        }

        private void PollButton(InputId inputId, InputEventPollingType inputEventPollingType)
        {
            string inputString = GeneratedInput.Ids[inputId];
            if (Input.GetButtonDown(inputString))
            {
                Receiver?.ReceiveButtonEvent(new InputButtonEvent(inputId, inputEventPollingType, InputEventType.Down));
            }

            if (Input.GetButtonUp(inputString))
            {
                Receiver?.ReceiveButtonEvent(new InputButtonEvent(inputId, inputEventPollingType, InputEventType.Up));
            }

            if (Input.GetButton(inputString))
            {
                Receiver?.ReceiveButtonEvent(new InputButtonEvent(inputId, inputEventPollingType, InputEventType.Hold));
            }
        }

        private void PollAxis(InputId inputId, InputEventPollingType inputEventPollingType)
        {
            string inputString = GeneratedInput.Ids[inputId];
            Receiver?.ReceiveAxisEvent(Input.GetAxis(inputString), new InputAxisEvent(inputId, inputEventPollingType));
        }
    }
}
