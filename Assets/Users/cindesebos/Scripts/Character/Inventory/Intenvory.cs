using System;
using System.Collections.Generic;
using Scripts.Items;
using Scripts.Sounds;
using UnityEngine;
using Scripts.Character;
using Zenject;

namespace Scripts.Character.Inventory
{
    public class Inventory : IInventory
    {
        public event Action<ItemData> OnItemPickedUp;

        private readonly SoundsContainer _soundsContainer;
        private readonly LazyInject<Character> _character;

        private List<ItemStack> _items = new();

        [Inject]
        public Inventory(SoundsContainer soundsContainer, LazyInject<Character> character)
        {
            Debug.Log("Started Injecting");

            _soundsContainer = soundsContainer;
            _character = character;

            Debug.Log($"Injected {_soundsContainer}  {_character}");
        }

        public bool TryAddItem(ItemData item)
        {
            if (item == null) return false;

            var stack = _items.Find(i => i.Item.Id == item.Id);

            if (stack != null) stack.Add();
            else _items.Add(new ItemStack(item));

            OnItemPickedUp?.Invoke(item);
            _character.Value.AudioSource.PlayOneShot(_soundsContainer.ItemPickupSound);
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
