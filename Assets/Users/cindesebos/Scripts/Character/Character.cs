using System;
using Scripts.Character.Inventory;
using Scripts.UI;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Scripts.Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterGravityHandler))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private bool _canRun = false;
        [SerializeField] private bool _haveGun = false;

        [field: SerializeField] public GunShooter GunShooter { get; private set; }
        [SerializeField] private Camera _targetCamera;
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private CharacterGravityHandler _gravityHandler;
        [SerializeField] private CharacterItemProvider _itemProvider;
        [SerializeField] private Transform _cameraHolder;

        private CharacterInput _input;
        private CharacterData _data;
        private IInventory _inventory;
        private ArtsToggler _artsToggler;
        private HudView _hudView;

        private void OnValidate()
        {
            _movement ??= GetComponent<CharacterMovement>();
            _controller ??= GetComponent<CharacterController>();
            _gravityHandler ??= GetComponent<CharacterGravityHandler>();
            _itemProvider ??= GetComponentInChildren<CharacterItemProvider>();
            GunShooter ??= GetComponentInChildren<GunShooter>();
        }

        [Inject]
        private void Construct(CharacterInput input, CharacterData data, IInventory inventory, ArtsToggler artsToggler, HudView hudView)
        {
            _input = input;
            _data = data;
            _inventory = inventory;
            _artsToggler = artsToggler;
            _hudView = hudView;
        }

        private void OnEnable()
        {
            _input.Enable();

            _input.Interaction.Use.performed += _itemProvider.UseItem;
            _artsToggler.OnShow += OnArtShow;
            _artsToggler.OnHide += OnArtHide;
        }

        private void Start()
        {
            _movement.Initialize(_input, _data, _controller, _cameraHolder, _canRun);
            _gravityHandler.Initialize(_data, _controller);
            _itemProvider.Initialize(_targetCamera.transform, _data, _inventory, _artsToggler);

            if (_haveGun) ActiveGun();
            else
            {
                GunShooter.gameObject.SetActive(false);
                _hudView.SetGunPanelActive(false);
            }
        }

        private void Update()
        {
            _movement.Rotate();
            _movement.Move();
            _movement.ApplyHeadBob();
            _itemProvider.Handle();
            _gravityHandler.Handle();
        }

        private void OnArtShow() => _input.Disable();

        private void OnArtHide() => EnableInputNextFrame();

        private async UniTaskVoid EnableInputNextFrame()
        {
            await UniTask.Yield();
            
            _input.Enable();
        }

        public void ActiveGun()
        {
            GunShooter.gameObject.SetActive(true);

            _hudView.SetGunPanelActive(true);

            GunShooter.Initialize(_hudView, _input);
        }

        public void ToggleGun()
        {
            if (!_haveGun) return;

            GunShooter.gameObject.SetActive(!GunShooter.gameObject.activeSelf);
        }

        private void OnDisable()
        {
            _artsToggler.OnShow -= OnArtShow;
            _artsToggler.OnHide -= OnArtHide;
            _input.Interaction.Use.performed -= _itemProvider.UseItem;

            _input.Disable();
        }
    }
}