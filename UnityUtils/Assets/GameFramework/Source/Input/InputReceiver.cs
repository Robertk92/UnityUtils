using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameFramework
{
    public delegate void InputButtonEventDelegate();
    public delegate void InputAxisEventDelegate(float axisValue);

    public sealed class InputReceiver : MonoBehaviour
    {
        private readonly Dictionary<InputButtonEvent, InputButtonEventDelegate> _buttonEvents = new Dictionary<InputButtonEvent, InputButtonEventDelegate>();
        private readonly Dictionary<InputAxisEvent, InputAxisEventDelegate> _axisEvents = new Dictionary<InputAxisEvent, InputAxisEventDelegate>();

        public void BindButton(string inputId, InputEventPollingType inputEventPollingType, InputEventType inputEventType, 
            InputButtonEventDelegate handler)
        {
            InputButtonEvent inputButtonEvent = new InputButtonEvent(inputId, inputEventPollingType, inputEventType);
            Debug.AssertFormat(!_buttonEvents.ContainsKey(inputButtonEvent), 
                string.Format("Button binding for '{0}' with the same parameters already bound", inputId));
            _buttonEvents.Add(inputButtonEvent, handler);
        }

        public void BindAxis(string inputId, InputEventPollingType inputEventPollingType,
            InputAxisEventDelegate handler)
        {
            InputAxisEvent inputAxisEvent = new InputAxisEvent(inputId, inputEventPollingType);
            Debug.AssertFormat(!_axisEvents.ContainsKey(inputAxisEvent), 
                string.Format("Axis binding for '{0}' with the same parameters already bound", inputId));
            _axisEvents.Add(inputAxisEvent, handler);
        }

        internal void ReceiveButtonEvent(InputButtonEvent inputButtonEvent)
        {
            foreach (KeyValuePair<InputButtonEvent, InputButtonEventDelegate> keyValuePair in _buttonEvents)
            {
                if (keyValuePair.Key != inputButtonEvent)
                {
                    continue;
                }

                keyValuePair.Value.Invoke();
                break;
            }
        }

        internal void ReceiveAxisEvent(float axisValue, InputAxisEvent inputAxisEvent)
        {
            foreach (KeyValuePair<InputAxisEvent, InputAxisEventDelegate> keyValuePair in _axisEvents)
            {
                if (keyValuePair.Key != inputAxisEvent)
                {
                    continue;
                }

                keyValuePair.Value.Invoke(axisValue);
                break;
            }
        }
    }
}
