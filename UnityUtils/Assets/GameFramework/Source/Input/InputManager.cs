using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public sealed class InputManager : MonoBehaviour
    {
        private InputReceiver _receiver;
        public InputReceiver Receiver
        {
            get { return _receiver; }
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

            PollButton("Fire", InputEventPollingType.FixedUpdate);
            PollAxis("Horizontal", InputEventPollingType.FixedUpdate);
        }

        private void Update()
        {
            if (Receiver == null)
            {
                return;
            }

            PollButton("Fire", InputEventPollingType.Update);
            PollAxis("Horizontal", InputEventPollingType.Update);
        }

        private void PollButton(string inputId, InputEventPollingType inputEventPollingType)
        {
            if (Input.GetButtonDown(inputId) && Receiver != null)
            {
                Receiver.ReceiveButtonEvent(new InputButtonEvent(inputId, inputEventPollingType, InputEventType.Down));
            }

            if (Input.GetButtonUp(inputId) && Receiver != null)
            {
                Receiver.ReceiveButtonEvent(new InputButtonEvent(inputId, inputEventPollingType, InputEventType.Up));
            }

            if (Input.GetButton(inputId) && Receiver != null)
            {
                Receiver.ReceiveButtonEvent(new InputButtonEvent(inputId, inputEventPollingType, InputEventType.Hold));
            }
        }

        private void PollAxis(string inputId, InputEventPollingType inputEventPollingType)
        {
            if (Receiver == null)
            {
                return;
            }

            Receiver.ReceiveAxisEvent(Input.GetAxis(inputId), new InputAxisEvent(inputId, inputEventPollingType));
        }
    }
}
