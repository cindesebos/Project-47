using System;
using UnityEngine;
using Zenject;

namespace Scripts.Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterGravityHandler))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private CharacterGravityHandler _gravityHandler;
        [SerializeField] private Transform _cameraHolder;

        private CharacterInput _input;
        private CharacterData _data;

        private void OnValidate()
        {
            _movement ??= GetComponent<CharacterMovement>();
            _controller ??= GetComponent<CharacterController>();
            _gravityHandler ??= GetComponent<CharacterGravityHandler>();
        }

        [Inject]
        private void Construct(CharacterInput input, CharacterData data)
        {
            _input = input;
            _data = data;
        }

        private void OnEnable() => _input.Enable();

        private void Start()
        {
            _movement.Initialize(_input, _data, _controller, _cameraHolder);
            _gravityHandler.Initialize(_data, _controller);
        }

        private void Update()
        {
            _movement.Rotate();
            _movement.Move();
            _gravityHandler.Handle();
        }

        private void OnDisable() => _input.Disable();
    }
}