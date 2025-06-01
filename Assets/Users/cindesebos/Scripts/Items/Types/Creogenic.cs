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
    public class Creogenic : InteractableItem
    {
        [SerializeField] private GameObject _newVisual;
        [SerializeField] private Outline _outline;

        private bool _canInteract = false;

        [Inject] private CharacterInput _characterInput;

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
            _newVisual.SetActive(true);

            gameObject.SetActive(false);
        }

        private void OnTriggerExit(Collider collider)
        {
            if(!collider.gameObject.GetComponent<Character.Character>())
            
            _outline.OutlineWidth = 0;

            if (_canInteract) _characterInput.Interaction.Use.performed -= Use;
        }
    }
}
