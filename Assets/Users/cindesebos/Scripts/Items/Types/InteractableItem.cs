using Scripts.Character.Inventory;
using Scripts.Items;
using UnityEngine;
using Zenject;

namespace Scripts.Items.Types
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class InteractableItem : MonoBehaviour
    {
        [field: SerializeField] public InteractableItemData Data { get; protected set; }

        protected int _targetItemId;

        private IInventory _inventory;

        [Inject]
        private void Construct(IInventory inventory)
        {
            _inventory = inventory;

            _targetItemId = Data.TargetItemId;

            _inventory.OnItemPickedUp += OnItemPickedUp;
        }

        protected virtual void OnItemPickedUp(ItemData item) { }

        private void ODestroy() => _inventory.OnItemPickedUp -= OnItemPickedUp;
    }
}