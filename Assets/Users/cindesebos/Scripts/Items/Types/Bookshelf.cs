using Scripts.Character.Inventory;
using UnityEngine;
using Zenject;

namespace Scripts.Items.Types
{
    public class Bookshelf : InteractableItem
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Item _keyCard;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        private void Start() => _keyCard.enabled = false;

        protected override void OnItemPickedUp(ItemData item)
        {
            if (_targetItemId != item.Id) return;

            _animator.enabled = true;
        }

        public void ActiveTargetItem() => _keyCard.enabled = true;
    }
}