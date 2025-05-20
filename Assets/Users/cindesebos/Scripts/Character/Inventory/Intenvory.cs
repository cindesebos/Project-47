using System.Collections.Generic;
using Scripts.Items;
using UnityEngine;

namespace Scripts.Character.Inventory
{
    public class Inventory : IInventory
    {
        private List<ItemStack> _items = new();

        public bool TryAddItem(ItemData item)
        {
            if (item == null) return false;

            var stack = _items.Find(i => i.Item.Id == item.Id);

            if (stack != null) stack.Add();
            else _items.Add(new ItemStack(item));

            return true;
        }

        public bool TryRemoveItem(ItemData item)
        {
            if (item == null) return false;

            var stack = _items.Find(i => i.Item.Id == item.Id);

            if (stack != null && stack.TryRemove())
            {
                if (stack.IsEmpty) _items.Remove(stack);

                return true;
            }

            return false;
        }
    }
}
