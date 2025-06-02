using Scripts.Character.Inventory;
using UnityEngine;
using Zenject;
using Scripts.Items;
using UnityEngine.InputSystem;
using System;
using Scripts.Props;
using Scripts.UI;

namespace Scripts.Items.Types
{
    [RequireComponent(typeof(Outline))]
    public class Mirror : InteractableItem
    {
        [SerializeField] private Sprite _art;
        [SerializeField] private GameObject _partToBreak;
        [SerializeField] private GameObject _brokenPart;
        [SerializeField] private Outline _outline;

        private bool _canUse = false;
        private bool _isUsed = false;

        private CharacterInput _characterInput;
        private ArtsToggler _artsToggler;
        
        [Inject]
        private void Construct(CharacterInput characterInput, ArtsToggler artsToggler)
        {
            _characterInput = characterInput;
            _artsToggler = artsToggler;
        }

        private void Start() => _outline.OutlineWidth = 0;

        private void OnValidate()
        {
            _outline ??= GetComponent<Outline>();
            if (_outline.OutlineWidth != 0) _outline.OutlineWidth = 0;
        }

        protected override void OnItemPickedUp(ItemData item)
        {
            if (_targetItemId != item.Id) return;

            _canUse = true;
        }

        private void OnTriggerEnter(Collider collider)
        {
            _outline.OutlineWidth = Data.OutlineWidth;

            if (_canUse && collider.gameObject.GetComponent<Character.Character>())
            {
                _characterInput.Interaction.Use.performed += Use;
            }
        }

        private void Use(InputAction.CallbackContext context)
        {
            if (_isUsed) return;

            _isUsed = true;

            _artsToggler.Show(_art);

            _brokenPart.SetActive(true);
        }

        private void OnTriggerExit(Collider collider)
        {
            _outline.OutlineWidth = 0;

            if (_canUse && collider.gameObject.GetComponent<Character.Character>())
            {
                _characterInput.Interaction.Use.performed -= Use;
            }
        }
    }
}