using UnityEngine;
using Scripts.UI;
using System;
using UnityEngine.InputSystem;
using Scripts.Character;
using Zenject.SpaceFighter;
using Scripts.Mutant;
using Scripts.Sounds;

namespace Scripts.Items.Gun
{
    public class GunShooter : MonoBehaviour, IAttacker
    {
        [SerializeField] private GunData _data;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private ParticleSystem _muzzleFlashParticle;
        [SerializeField] private LineRenderer _bulletLineRendererPrefab;
        [SerializeField] private LayerMask _bodyPartsLayer;

        private HudView _hudView;
        private CharacterInput _input;

        private int _currentAmmoAmount = 0;
        private float _range;
        private float _damage;
        private float _bulletSpeed;
        private Camera _camera;
        private SoundsContainer _soundsContainer;
        private AudioSource _audioSource;

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

        public void Initialize(HudView hudView, CharacterInput input, SoundsContainer soundsContainer, AudioSource audioSource)
        {
            _camera = Camera.main;

            _input = input;

            _hudView = hudView;

            _range = _data.Range;
            Damage = _data.Damage;
            _bulletSpeed = _data.BulletSpeed;
            _camera = Camera.main;
            _soundsContainer = soundsContainer;
            _audioSource = audioSource;

            OnAmmoAmountChanged();

            _input.Movement.Shoot.performed += OnShoot;
        }

        private void OnEnable()
        {
            if (_input == null) return;

            _input.Movement.Shoot.performed += OnShoot;
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (CurrentAmmoAmount <= 0) return;

            Shoot();

            _audioSource.PlayOneShot(_soundsContainer.GunShootingSound);

            CurrentAmmoAmount--;
        }

        private void Shoot()
        {
            RaycastHit hit;
            Vector3 origin = _camera.transform.position;
            Vector3 direction = _camera.transform.forward;

            Vector3 hitPoint;

            _muzzleFlashParticle.Play();

            if (Physics.Raycast(origin, direction, out hit, _range, _bodyPartsLayer))
            {
                hitPoint = hit.point;

                var bodyPart = hit.collider.GetComponent<BodyPartDamageMultiplier>();

                Debug.Log($"shot in {hit.collider.name}");

                if (bodyPart)
                {
                    Debug.Log($"body part is {bodyPart.name}");

                    float calculatedDamage = bodyPart.GetCalculatedDamage(Damage);

                    ApplyAttack(bodyPart.MutantHealth, calculatedDamage);
                }
            }
            else
            {
                hitPoint = origin + direction * _range;
            }

            if (_bulletLineRendererPrefab != null)
            {
                LineRenderer lineRenderer = Instantiate(_bulletLineRendererPrefab);

                if (lineRenderer != null)
                {
                    lineRenderer.SetPosition(0, _spawnPosition.position);
                    lineRenderer.SetPosition(1, hitPoint);
                }

                Destroy(lineRenderer.gameObject, 0.05f);
            }
        }

        public void AddAmmo(int amount) => CurrentAmmoAmount += amount;

        private void OnAmmoAmountChanged() => _hudView?.SetAmmoAmount(CurrentAmmoAmount);

        public void SetAmmo(int amount) => CurrentAmmoAmount = amount;

        private void OnDisable()
        {
            if (_input == null) return;

            _input.Movement.Shoot.performed -= OnShoot;
        }

        public void ApplyAttack(IDamageable target, float calculatedDamage) => target.ApplyDamage(calculatedDamage);
    }
}
