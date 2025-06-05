using Scripts.Character.Inventory;
using UnityEngine;
using Zenject;
using Scripts.Items;
using UnityEngine.InputSystem;
using System;
using Scripts.Props;
using Scripts.Sounds;

namespace Scripts.Items.Types
{
    [RequireComponent(typeof(Outline))]
    public class Blockage : InteractableItem
    {
        [SerializeField] private GameObject _parent;
        [SerializeField] private Outline _outline;
        [SerializeField] private AudioSource _audioSource;

        private bool _canInteract = false;
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

            _canInteract = true;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!collider.gameObject.GetComponent<Character.Character>()) return;

            _outline.OutlineWidth = Data.OutlineWidth;

            if (_canInteract) _characterInput.Interaction.Use.performed += Use;
        }

        private void Use(InputAction.CallbackContext context)
        {
            if(_isUsed) return;

             _isUsed = true;

            _parent.SetActive(false);
            _audioSource.PlayOneShot(_soundsContainer.ExplosionSound);
        }

        private void OnTriggerExit(Collider collider)
        {
            if(!collider.gameObject.GetComponent<Character.Character>())

            _outline.OutlineWidth = 0;

            if (_canInteract) _characterInput.Interaction.Use.performed -= Use;
        }
    }
}