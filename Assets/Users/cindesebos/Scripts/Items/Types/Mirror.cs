using Scripts.Character.Inventory;
using UnityEngine;
using Zenject;
using Scripts.Items;
using UnityEngine.InputSystem;
using System;
using Scripts.Props;

namespace Scripts.Items.Types
{
    [RequireComponent(typeof(Outline))]
    public class Mirror : InteractableItem
    {
        [SerializeField] private GameObject _partToBreak;
        [SerializeField] private GameObject _brokenPart;
        [SerializeField] private Outline _outline;

        private bool _canUse = false;
        private bool _isUsed = false;

        [Inject] private CharacterInput _characterInput;

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