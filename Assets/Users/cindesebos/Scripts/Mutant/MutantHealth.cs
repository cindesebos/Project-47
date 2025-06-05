using UnityEngine;
using System;
using Scripts.Sounds;

namespace Scripts.Mutant
{
    public class MutantHealth : MonoBehaviour, IDamageable
    {
        public event Action OnAppliedDamage;

        private float _health;
        private AudioSource _audioSource;
        private SoundsContainer _soundsContainer;

        public float Health
        {
            get => _health;
            set
            {
                if (value < 0) value = 0;

                _health = value;

                Debug.Log($"Mutant has {_health} health");

                if (_health <= 0) gameObject.SetActive(false);
            }
        }

        private MutantData _data;

        public void Initialize(MutantData data, AudioSource audioSource, SoundsContainer soundsContainer)
        {
            _data = data;
            _audioSource = audioSource;
            _soundsContainer = soundsContainer;

            Health = _data.Health;
        }

        public void ApplyDamage(float damage)
        {
            _audioSource.PlayOneShot(_soundsContainer.MutantTakingDamage);

            Health -= damage;

            OnAppliedDamage?.Invoke();
        }
    }
}