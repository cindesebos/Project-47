using UnityEngine;
using Zenject;
using UnityEngine.InputSystem;
using System;
using Scripts.Items.Simply;

namespace Scripts.Items
{
    public class ItemsCaller : IDisposable
    {
        private readonly CharacterInput _characterInput;
        private readonly Character.Character _character;
        private readonly FirstAidKitLogic _firstAidKitLogic;

        public ItemsCaller(CharacterInput characterInput, Character.Character character, FirstAidKitLogic firstAidKitLogic)
        {
            _characterInput = characterInput;
            _character = character;
            _firstAidKitLogic = firstAidKitLogic;

            _characterInput.Interaction.GunToggle.performed += OnGunTogglePerformed;
            _characterInput.Interaction.FirstAidKitUsing.performed += OnFirstAidKitUsingPerformed;
        }

        private void OnGunTogglePerformed(InputAction.CallbackContext context)
        {
            _character.ToggleGun();
        }

        private void OnFirstAidKitUsingPerformed(InputAction.CallbackContext context)
        {
            _firstAidKitLogic.TryRemove(1);
        }

        public void Dispose()
        {
            _characterInput.Interaction.GunToggle.performed -= OnGunTogglePerformed;
            _characterInput.Interaction.FirstAidKitUsing.performed -= OnFirstAidKitUsingPerformed;
        }
    }
}