using System.Collections.Generic;
using Rewired;
using UnityEngine;

namespace GameFramework
{
    public delegate void InputButtonEventDelegate();
    public delegate void InputAxisEventDelegate(float axisValue);

    public sealed class InputReceiver : MonoBehaviour
    {
        public InputPlayer InputPlayer;

        private readonly Dictionary<InputActionEvent, InputButtonEventDelegate> _buttonEvents = new Dictionary<InputActionEvent, InputButtonEventDelegate>();
        private readonly Dictionary<InputAxisEvent, InputAxisEventDelegate> _axisEvents = new Dictionary<InputAxisEvent, InputAxisEventDelegate>();

        private PlayerInputController _playerInputController;

        private void Start()
        {
            _playerInputController = FindObjectOfType<PlayerInputController>();
        }

        public void BindButton(int actionId, InputEventPollingType inputEventPollingType, InputEventType inputEventType,
            InputButtonEventDelegate handler)
        {
            InputActionEvent inputActionEvent = new InputActionEvent(actionId, inputEventPollingType, inputEventType);
            Debug.AssertFormat(!_buttonEvents.ContainsKey(inputActionEvent), $"Button binding for '{actionId}' with the same parameters already bound");
            _buttonEvents.Add(inputActionEvent, handler);
        }

        public void BindAxis(int actionId, InputEventPollingType inputEventPollingType,
            InputAxisEventDelegate handler)
        {
            InputAxisEvent inputAxisEvent = new InputAxisEvent(actionId, inputEventPollingType);
            Debug.AssertFormat(!_axisEvents.ContainsKey(inputAxisEvent), $"Axis binding for '{actionId}' with the same parameters already bound");
            _axisEvents.Add(inputAxisEvent, handler);
        }

        /// <summary>
        /// </summary>
        /// <param name="motorIndex"></param>
        /// <param name="amount">Intensity of the rumble (between 0.0f and 1.0f)</param>
        /// <param name="duration">The duration of the rumble (0.0f means indefinite)</param>
        public void SetRumble(int motorIndex, float amount, float duration = 0.0f)
        {
            _playerInputController.SetRumble(InputPlayer, motorIndex, amount, duration);
        }

        public void StopRumble()
        {
            _playerInputController.StopRumble(InputPlayer);
        }

        internal void ReceiveButtonEvent(InputActionEvent inputActionEvent)
        {
            foreach (KeyValuePair<InputActionEvent, InputButtonEventDelegate> keyValuePair in _buttonEvents)
            {
                if (keyValuePair.Key != inputActionEvent)
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
