using UnityEngine;

namespace Scripts.Character
{
    public class CharacterGravityHandler : MonoBehaviour
    {
        private const float MinYVelocity = -2f;

        private float _gravity;

        private CharacterData _data;
        private CharacterController _controller;

        private Vector3 _velocity;

        public void Initialize(CharacterData data, CharacterController controller)
        {
            _data = data;
            _controller = controller;

            _gravity = _data.Gravity;
        }

        public void Handle()
        {
            _velocity.y += _gravity * Time.deltaTime;

            _controller.Move(_velocity * Time.deltaTime);

            if (_controller.isGrounded && _velocity.y < 0) _velocity.y = MinYVelocity;
        }
    }
}