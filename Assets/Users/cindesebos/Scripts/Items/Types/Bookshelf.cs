using Scripts.Character.Inventory;
using UnityEngine;
using Zenject;

namespace Scripts.Items.Types
{
    public class Bookshelf : InteractableItem
    {
        [SerializeField] private BoxCollider[] _collidersToEnable;
        [SerializeField] private Animator _animator;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        private void Start() => _animator.enabled = false;

        protected override void OnItemPickedUp(ItemData item)
        {
            if (_targetItemId != item.Id) return;

            _animator.enabled = true;

            foreach(var collider in _collidersToEnable) collider.enabled = true;
        }
    }
}