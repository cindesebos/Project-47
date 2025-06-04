using Scripts.Character.Inventory;
using UnityEngine;
using Zenject;
using Scripts.Items;
using UnityEngine.InputSystem;
using System;
using Scripts.Props;
using Scripts.Sounds;
using Cysharp.Threading.Tasks;

namespace Scripts.Items.Types
{
    [RequireComponent(typeof(Outline))]
    public class HandScanner : InteractableItem
    {
        private const float DelayBeforeDoorOpening = 3f;

        [SerializeField] private ClosedDoor _door;
        [SerializeField] private Collider _exoskeletonCollider;
        [SerializeField] private GameObject _glass;
        [SerializeField] private Outline _outline;
        [SerializeField] private AudioSource _audioSource;

        private bool _canOpenDoor = false;
        private bool _isUsed = false;

        private CharacterInput _characterInput;
        private SoundsContainer _soundsContainer;

        [Inject]
        private void Construct(SoundsContainer soundsContainer, CharacterInput characterInput)
        {
            _characterInput = characterInput; 
            _soundsContainer = soundsContainer;
        }

        private void OnValidate()
        {
            _outline ??= GetComponent<Outline>();
            if (_outline.OutlineWidth != 0) _outline.OutlineWidth = 0;
        }

        private void Start() => _outline.OutlineWidth = 0;

        protected override void OnItemPickedUp(ItemData item)
        {
            if (_targetItemId != item.Id) return;

            _canOpenDoor = true;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (_isUsed) return;
            
            if (!collider.gameObject.GetComponent<Character.Character>())

                _outline.OutlineWidth = Data.OutlineWidth;

            if (_canOpenDoor) _characterInput.Interaction.Use.performed += Use;
        }

        private void Use(InputAction.CallbackContext context)
        {
            if (_isUsed) return;

            _isUsed = true;
            _audioSource.PlayOneShot(_soundsContainer.ScanningHandAndEyeSound);

            _outline.OutlineWidth = 0;

            OpenDoorWithDelay().Forget();
        }

        private async UniTaskVoid OpenDoorWithDelay()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(DelayBeforeDoorOpening));

            _door?.Open();
            if (_exoskeletonCollider != null) _exoskeletonCollider.enabled = true;
            _glass?.SetActive(false);
        }

        private void OnTriggerExit(Collider collider)
        {
            if (!collider.gameObject.GetComponent<Character.Character>())

                _outline.OutlineWidth = 0;

            if (_canOpenDoor) _characterInput.Interaction.Use.performed -= Use;
        }
    }
}