using Scripts.Character.Inventory;
using UnityEngine;
using Zenject;
using Scripts.Items;
using UnityEngine.InputSystem;
using System;
using Scripts.Props;
using Scripts.Sounds;
using Scripts.UI;

namespace Scripts.Items.Types
{
    [RequireComponent(typeof(Outline))]
    public class Doctor : InteractableItem
    {
        [SerializeField] private Sprite[] _arts;
        [SerializeField] private GameObject _modelToShow;
        [SerializeField] private Outline _outline;
        [SerializeField] private AudioSource _audioSource;

        private bool _canInteract = false;
        private bool _isUsed;
        
        private CharacterInput _characterInput;
        private SoundsContainer _soundsContainer;
        private ArtsToggler _artsToggler;

        [Inject]
        private void Construct(CharacterInput characterInput, SoundsContainer soundsContainer, ArtsToggler artsToggler)
        {
            _characterInput = characterInput;
            _soundsContainer = soundsContainer;
            _artsToggler = artsToggler;
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
            if (!collider.gameObject.GetComponent<Character.Character>() && _isUsed) return;

            _outline.OutlineWidth = Data.OutlineWidth;

            if (_canInteract) _characterInput.Interaction.Use.performed += Use;
        }

        private void Use(InputAction.CallbackContext context)
        {
            if (_isUsed) return;
            
            _isUsed = true;

            _audioSource.PlayOneShot(_soundsContainer.RemovingHandSound);
            _outline.OutlineWidth = 0;

            _modelToShow.SetActive(true);
            _artsToggler.Show(_arts);

            gameObject.SetActive(false);
        }

        private void OnTriggerExit(Collider collider)
        {
            if (!collider.gameObject.GetComponent<Character.Character>() && _isUsed) return;
            
            _outline.OutlineWidth = 0;

            if (_canInteract) _characterInput.Interaction.Use.performed -= Use;
        }
    }
}
