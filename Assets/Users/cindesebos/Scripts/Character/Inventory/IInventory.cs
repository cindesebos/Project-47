using Scripts.Items;

namespace Scripts.Character.Inventory
{
    public interface IInventory
    {
        bool TryAddItem(ItemData item);

        bool TryRemoveItem(ItemData item);
    }
}
