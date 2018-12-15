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
            InputReceiver.BindButton("Fire", InputEventPollingType.Update, InputEventType.Down, OnFireDown);
            
            InputReceiver.BindButton("Fire", InputEventPollingType.Update, InputEventType.Hold, OnFireHold);
            InputReceiver.BindButton("Fire", InputEventPollingType.Update, InputEventType.Up, OnFireUp);

            InputReceiver.BindAxis("Horizontal", InputEventPollingType.Update, OnHorizontal);
        }

        private void OnHorizontal(float axisvalue)
        {
            Debug.Log(axisvalue);
        }

        private void OnFireHold()
        {
            Debug.Log("Fire holding...");
        }

        private void OnFireUp()
        {
            Debug.Log("Fire up");
        }

        private void OnFireDown()
        {
            Debug.Log("Fire down");
        }
    }
}
