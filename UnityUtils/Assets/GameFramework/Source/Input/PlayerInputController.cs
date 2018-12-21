using System;
using System.Collections.Generic;
using UnityEngine;

using ReInput = Rewired.ReInput;

namespace GameFramework
{
    public sealed class PlayerInputController : MonoBehaviour
    {
        private Dictionary<InputPlayer, InputReceiver> _receivers = new Dictionary<InputPlayer, InputReceiver>();
        public IEnumerable<KeyValuePair<InputPlayer, InputReceiver>> Receivers => _receivers;

        private void Awake()
        {
            foreach (InputPlayer player in Enum.GetValues(typeof(InputPlayer)))
            {
                _receivers.Add(player, null);
            }
        }

        public void Possess(InputReceiver receiver)
        {
            _receivers[receiver.InputPlayer] = receiver;
        }

        public void UnPossess(InputReceiver receiver)
        {
            foreach (InputPlayer key in _receivers.Keys)
            {
                if (_receivers[key] == receiver)
                {
                    _receivers[key] = null;
                }
            }
        }
        
        public void SetRumble(InputPlayer player, int motorIndex, float amount, float duration = 0.0f)
        {
            InputReceiver receiver = _receivers[player];
            if (receiver == null)
            {
                return;
            }

            if (duration > 0.0f)
            {
                ReInput.players.GetPlayer((int)receiver.InputPlayer).SetVibration(motorIndex, amount, duration);
            }
            else
            {
                ReInput.players.GetPlayer((int)receiver.InputPlayer).SetVibration(motorIndex, amount);
            }
        }
        
        public void StopRumble(InputPlayer player)
        {
            InputReceiver receiver = _receivers[player];
            if (receiver == null)
            {
                return;
            }

            ReInput.players.GetPlayer((int)receiver.InputPlayer).StopVibration();
        }

        private void Update()
        {
            foreach (InputPlayer player in _receivers.Keys)
            {
                Poll(player, InputEventPollingType.Update);
            }
        }

        private void FixedUpdate()
        {
            foreach (InputPlayer player in _receivers.Keys)
            {
                Poll(player, InputEventPollingType.FixedUpdate);
            }
        }

        private void Poll(InputPlayer player, InputEventPollingType pollingType)
        {
            InputReceiver receiver = _receivers[player];
            if (receiver == null)
            {
                return;
            }

            Rewired.Player rePlayer = ReInput.players.GetPlayer((int)receiver.InputPlayer);
            foreach (var action in ReInput.mapping.Actions)
            {
                if (rePlayer.GetButtonDown(action.id))
                {
                    _receivers[player]?.ReceiveButtonEvent(new InputActionEvent(action.id, pollingType, InputEventType.Down));
                }
                if (rePlayer.GetButtonUp(action.id))
                {
                    _receivers[player]?.ReceiveButtonEvent(new InputActionEvent(action.id, pollingType, InputEventType.Up));
                }
                if (rePlayer.GetButton(action.id))
                {
                    _receivers[player]?.ReceiveButtonEvent(new InputActionEvent(action.id, pollingType, InputEventType.Hold));
                }

                _receivers[player]?.ReceiveAxisEvent(rePlayer.GetAxis(action.id), new InputAxisEvent(action.id, pollingType));
            }
        }


    }
}
