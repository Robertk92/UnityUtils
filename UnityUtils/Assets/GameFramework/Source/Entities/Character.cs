using UnityEngine;

namespace GameFramework
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour
    {
        public float GravityScale = 1.0f;

        public CollisionFlags CollisionFlags { get; private set; }

        private CharacterController _controller;
        private float _downwardsVelocity;
        private Vector3 _frameMotion;
        

        protected virtual void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        protected virtual void Update()
        {
            if(CollisionFlags.HasFlag(CollisionFlags.Below))
            {
                _downwardsVelocity = 0.0f;
            }

            _downwardsVelocity += Mathf.Abs(Physics.gravity.y) * Time.deltaTime;
            _frameMotion += Vector3.down * GravityScale * _downwardsVelocity * Time.deltaTime;
            CollisionFlags = _controller.Move(_frameMotion);
            _frameMotion = Vector3.zero;
        }

        public void AddMovement(Vector3 motion)
        {
            _frameMotion += motion;
        }
    }
}
