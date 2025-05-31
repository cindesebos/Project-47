using UnityEngine;
using Scripts.UI;
using System;
using UnityEngine.InputSystem;
using Scripts.Character;
using Zenject.SpaceFighter;
using Scripts.Mutant;

namespace Scripts.Items.Gun
{
    public class GunShooter : MonoBehaviour, IAttacker
    {
        [SerializeField] private GunData _data;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;

        private HudView _hudView;
        private CharacterInput _input;

        private int _currentAmmoAmount = 0;
        private float _range;
        private float _damage;
        private float _bulletSpeed;
        private Camera _camera;

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

        public float Damage { get; private set; }

        public void Initialize(HudView hudView, CharacterInput input)
        {
            _camera = Camera.main;

            _input = input;

            _hudView = hudView;

            _range = _data.Range;
            Damage = _data.Damage;
            _bulletSpeed = _data.BulletSpeed;
            _camera = Camera.main;

            OnAmmoAmountChanged();

            _input.Movement.Shoot.performed += OnShoot;
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (CurrentAmmoAmount <= 0) return;

            Shoot();

            CurrentAmmoAmount--;
        }

        private void Shoot()
        {
            RaycastHit hit;
            Vector3 origin = _camera.transform.position;
            Vector3 direction = _camera.transform.forward;

            Vector3 hitPoint;

            if (Physics.Raycast(origin, direction, out hit, _range))
            {
                hitPoint = hit.point;

                var bodyPart = hit.collider.GetComponent<BodyPartDamageMultiplier>();

                if (!bodyPart) return;

                float calculatedDamage = bodyPart.GetCalculatedDamage(Damage);

                ApplyAttack(bodyPart.MutantHealth, calculatedDamage);
            }
            else
            {
                hitPoint = origin + direction * _range;
            }
        }

        public void AddAmmo(int amount) => CurrentAmmoAmount += amount;

        private void OnAmmoAmountChanged() => _hudView?.SetAmmoAmount(CurrentAmmoAmount);

        public void SetAmmo(int amount) => CurrentAmmoAmount = amount;

        private void OnDestroy()
        {
            if (_input == null) return;

            _input.Movement.Shoot.performed -= OnShoot;
        }

        public void ApplyAttack(IDamageable target, float calculatedDamage) => target.ApplyDamage(calculatedDamage);
    }
}
