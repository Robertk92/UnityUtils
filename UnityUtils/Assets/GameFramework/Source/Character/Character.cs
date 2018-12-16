using UnityEngine;
using GameFramework;

namespace GameFramework
{
    [RequireComponent(typeof(InputReceiver), typeof(CharacterController))]
    public class Character : MonoBehaviour
    {
        protected InputReceiver InputReceiver { get; private set; }
        
        protected virtual void Awake()
        {
            InputReceiver = GetComponent<InputReceiver>();
            InputReceiver.BindButton(InputId.Fire, InputEventPollingType.Update, InputEventType.Down, OnFireDown);
            
            InputReceiver.BindButton(InputId.Fire, InputEventPollingType.Update, InputEventType.Hold, OnFireHold);
            InputReceiver.BindButton(InputId.Fire, InputEventPollingType.Update, InputEventType.Up, OnFireUp);

            InputReceiver.BindAxis(InputId.Horizontal, InputEventPollingType.Update, OnHorizontal);
        }

        private void OnHorizontal(float axisValue)
        {
            
        }

        private void OnFireHold()
        {
            
        }

        private void OnFireUp()
        {
            Debug.Log("Fire up");
        }

        private void OnFireDown()
        {
            
        }
    }
}
