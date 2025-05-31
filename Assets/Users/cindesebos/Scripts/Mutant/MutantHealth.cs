using UnityEngine;

namespace Scripts.Mutant
{
    public class MutantHealth : MonoBehaviour, IDamageable
    {
        private float _health;

        public float Health
        {
            get => _health;
            set
            {
                if (value < 0) value = 0;

                _health = value;

                Debug.Log($"Mutant has {_health} health");
            }
        }

        private MutantData _data;

        public void Initialize(MutantData data)
        {
            _data = data;

            Health = _data.Health;
        }

        public void ApplyDamage(float damage) => Health -= damage;
    }
}