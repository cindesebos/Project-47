using System;
using UnityEngine;

namespace Scripts.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        private float _walkSpeed;
        private float _runSpeed;
        private float _mouseSensitivity;
        private float _bobSpeed;
        private Vector3 _bobAmount;
        private float _minPitch;
        private float _maxPitch;

        private CharacterInput _input;
        private CharacterData _data;
        private CharacterController _controller;
        private Transform _cameraHolder;

        private float _xRotation = 0f;
        private bool _isRuning;

        public void Initialize(CharacterInput input, CharacterData data, CharacterController controller, Transform cameraHolder)
        {
            _input = input;
            _data = data;
            _controller = controller;
            _cameraHolder = cameraHolder;

            InitializeFields();
        }

        private void InitializeFields()
        {
            _walkSpeed = _data.WalkSpeed;
            _runSpeed = _data.RunSpeed;
            _mouseSensitivity = _data.MouseSensativity;
            _bobSpeed = _data.BobSpeed;
            _bobAmount = _data.BobAmount;
            _minPitch = _data.MinPitch;
            _maxPitch = _data.MaxPitch;
        }

        public void Rotate()
        {
            Vector2 scaledLookInput = ReadLookInput() * _mouseSensitivity;

            _xRotation -= scaledLookInput.y;
            _xRotation = Mathf.Clamp(_xRotation, _minPitch, _maxPitch);

            _cameraHolder.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * scaledLookInput.x);
        }

        public void Move()
        {
            Vector2 moveInput = ReadMoveInput();

            Vector3 direction = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;

            _controller.Move(direction * GetScaledSpeed(GetCurrentSpeed()));
        }

        private Vector2 ReadMoveInput() => _input.Movement.Move.ReadValue<Vector2>();

        private Vector2 ReadLookInput() => _input.Movement.Look.ReadValue<Vector2>();

        private float GetCurrentSpeed()
        {
            _isRuning = _input.Movement.Sprint.IsPressed();

            return _isRuning ? _runSpeed : _walkSpeed;
        }

        private float GetScaledSpeed(float currentSpeed) => currentSpeed * Time.deltaTime;
    }
}
