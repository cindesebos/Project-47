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
        private bool _isMoving;
        private bool _isRuning;
        private float _timer = 0f;
        private Vector3 _cameraHolderOrigin;

        public void Initialize(CharacterInput input, CharacterData data, CharacterController controller, Transform cameraHolder)
        {
            _input = input;
            _data = data;
            _controller = controller;
            _cameraHolder = cameraHolder;
            _cameraHolderOrigin = _cameraHolder.localPosition;

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

            if (moveInput.normalized.magnitude != 0) _isMoving = true;
            else _isMoving = false;

            Vector3 direction = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;

            _controller.Move(direction * GetScaledSpeed(GetCurrentSpeed()));
        }

        public void ApplyHeadBob()
        {
            if (_isMoving)
            {
                if (_isRuning) _timer += Time.deltaTime * (_bobSpeed + _runSpeed);
                else _timer += Time.deltaTime * _bobSpeed;

                float sin = Mathf.Sin(_timer);
                _cameraHolder.localPosition = _cameraHolderOrigin + sin * _bobAmount;
            }
            else
            {
                _timer = 0f;
                _cameraHolder.localPosition = Vector3.Lerp(_cameraHolder.localPosition, _cameraHolderOrigin, Time.deltaTime * _bobSpeed);
            }
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