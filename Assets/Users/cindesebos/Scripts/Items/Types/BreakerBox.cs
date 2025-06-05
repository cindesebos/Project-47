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
    public class BreakerBox : InteractableItem
    {
        private const float DelayBeforeDoorOpening = 3f;

        [SerializeField] private ClosedDoor _door;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Outline _outline;

        private bool _canOpenDoor = false;
        private bool _isUsed = false;

        private CharacterInput _characterInput;
        private SoundsContainer _soundsContainer;

        private void OnValidate()
        {
            _outline ??= GetComponent<Outline>();
            _audioSource ??= GetComponent<AudioSource>();

            if (_outline.OutlineWidth != 0) _outline.OutlineWidth = 0;
        }

        [Inject]
        private void Construct(SoundsContainer soundsContainer, CharacterInput characterInput)
        {
            _characterInput = characterInput; 
            _soundsContainer = soundsContainer;
        }

        private void Start() => _outline.OutlineWidth = 0;

        protected override void OnItemPickedUp(ItemData item)
        {
            if (_targetItemId != item.Id) return;

            _canOpenDoor = true;

            _outline.OutlineWidth = 0;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!collider.gameObject.GetComponent<Character.Character>() && _isUsed) return;

            _outline.OutlineWidth = Data.OutlineWidth;

            if (_canOpenDoor) _characterInput.Interaction.Use.performed += Use;
        }

        private void Use(InputAction.CallbackContext context)
        {
            if (_isUsed) return;

            _isUsed = true;
            _audioSource.PlayOneShot(_soundsContainer.FuseChangingSound);
            _outline.OutlineWidth = 0;

            OpenDoorWithDelay().Forget();
        }
        
        private async UniTaskVoid OpenDoorWithDelay()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(DelayBeforeDoorOpening));

            _door.Open();
        }

        private void OnTriggerExit(Collider collider)
        {
            if (!collider.gameObject.GetComponent<Character.Character>()) return;

            _outline.OutlineWidth = 0;

            if (_canOpenDoor) _characterInput.Interaction.Use.performed -= Use;
        }
    }
}
