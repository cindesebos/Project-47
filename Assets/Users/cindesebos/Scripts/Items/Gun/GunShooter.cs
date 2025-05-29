using UnityEngine;
using Scripts.UI;
using System;
using UnityEngine.InputSystem;

namespace Scripts
{
    public class GunShooter : MonoBehaviour
    {
        private HudView _hudView;
        private CharacterInput _input;

        private int _currentAmmoAmount = 0;

        public int CurrentAmmoAmount
        {
            get => _currentAmmoAmount;
            private set
            {
                if (value < 0) value = 0;

                _currentAmmoAmount = value;

                OnAmmoAmountChanged();
            }
        }

        public void Initialize(HudView hudView, CharacterInput input)
        {
            _input = input;

            _hudView = hudView;

            OnAmmoAmountChanged();

            _input.Movement.Shoot.performed += OnShoot;
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (CurrentAmmoAmount <= 0) return;

            CurrentAmmoAmount--;
        }

        public void AddAmmo(int amount) => CurrentAmmoAmount += amount;

        private void OnAmmoAmountChanged() => _hudView.SetAmmoAmount(CurrentAmmoAmount);

        private void OnDestroy()
        {
            _input.Movement.Shoot.performed -= OnShoot;
        }
    }
}
