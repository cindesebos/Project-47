using System;
using Scripts.Character.Inventory;
using UnityEngine;
using Zenject;

namespace Scripts.Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterGravityHandler))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private Camera _targetCamera;
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private CharacterGravityHandler _gravityHandler;
        [SerializeField] private CharacterItemProvider _itemProvider;
        [SerializeField] private Transform _cameraHolder;

        private CharacterInput _input;
        private CharacterData _data;
        private IInventory _inventory;

        private void OnValidate()
        {
            _movement ??= GetComponent<CharacterMovement>();
            _controller ??= GetComponent<CharacterController>();
            _gravityHandler ??= GetComponent<CharacterGravityHandler>();
            _itemProvider ??= GetComponentInChildren<CharacterItemProvider>();
        }

        [Inject]
        private void Construct(CharacterInput input, CharacterData data, IInventory inventory)
        {
            _input = input;
            _data = data;
            _inventory = inventory;
        }

        private void OnEnable()
        {
            _input.Enable();

            _input.Interaction.Use.performed += _itemProvider.UseItem;
        }

        private void Start()
        {
            _movement.Initialize(_input, _data, _controller, _cameraHolder);
            _gravityHandler.Initialize(_data, _controller);
            _itemProvider.Initialize(_targetCamera.transform, _data, _inventory);
        }

        private void Update()
        {
            _movement.Rotate();
            _movement.Move();
            _movement.ApplyHeadBob();
            _itemProvider.Handle();
            _gravityHandler.Handle();
        }

        private void OnDisable()
        {
            _input.Interaction.Use.performed -= _itemProvider.UseItem;

            _input.Disable();
        }
    }
}