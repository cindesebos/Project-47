using Scripts.Items;

namespace Scripts.Character.Inventory
{
    [System.Serializable]
    public class ItemStack
    {
        public ItemData Item;
        public int Amount;

        public ItemStack(ItemData item)
        {
            Item = item;
            Amount = 1;
        }

        public void Add() => Amount++;

        public bool TryRemove()
        {
            if (Amount > 0)
            {
                Amount--;

                return true;
            }

            return false;
        }

        public bool IsEmpty => Amount <= 0;
    }
}
