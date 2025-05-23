using Scripts.Items;
using System;

namespace Scripts.Character.Inventory
{
    public interface IInventory
    {
        event Action<ItemData> OnItemPickedUp;

        bool TryAddItem(ItemData item);

        bool TryRemoveItem(ItemData item);
    }
}
