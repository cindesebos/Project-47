using Scripts.Items;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using Zenject;
using Scripts.UI;
using Scripts.Items.Simply;

namespace Scripts.Character.Inventory
{
    public class InventorySlotsHandler : IDisposable
    {
        private readonly IInventory _inventory;
        private readonly Character _character;
        private readonly HudView _hudView;
        private readonly FirstAidKitLogic _firstAidKitLogic;

        private readonly string _gunItemName;
        private readonly string _ammoItemName;
        private readonly int _ammoAmount;
        private readonly string _firstAidKitItemName;
        private readonly int _firstAidKitAmount;

        [Inject]
        public InventorySlotsHandler(IInventory inventory, Character character, InventorySlotsHandlerData data, FirstAidKitLogic firstAidKitLogic)
        {
            Debug.Log("InventorySlotsHandler initialized");

            _inventory = inventory;
            _character = character;
            _firstAidKitLogic = firstAidKitLogic;

            _gunItemName = data.GunItemName;
            _ammoItemName = data.AmmoItemName;
            _ammoAmount = data.AmmoAmount;
            _firstAidKitItemName = data.FirstAidKitItemName;
            _firstAidKitAmount = data.FirstAidKitAmount;

            _inventory.OnItemPickedUp += OnItemPickedUp;
        }

        private void OnItemPickedUp(ItemData item)
        {
            Debug.Log($"Item picked up: {item.Name}");

            if (item.Name == _gunItemName) OnGunPickedUp();
            else if (item.Name == _ammoItemName) OnAmmoPickedUp();
            else if (item.Name == _firstAidKitItemName) OnFirstAidKitPickedUp();
        }

        private void OnGunPickedUp() => _character.ActiveGun();

        private void OnAmmoPickedUp() => _character.GunShooter.AddAmmo(_ammoAmount);

        private void OnFirstAidKitPickedUp() => _firstAidKitLogic.Add(_firstAidKitAmount);

        public void Dispose()
        {
            _inventory.OnItemPickedUp -= OnItemPickedUp;
        }
    }
}
