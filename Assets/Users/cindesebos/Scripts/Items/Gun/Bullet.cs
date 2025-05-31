using Scripts.Mutant;
using UnityEngine;

namespace Scripts.Items.Gun
{
    public class Bullet : MonoBehaviour, IAttacker
    {
        private const float LifeTime = 4f;

        public float Damage { get; private set; }

        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        public void Initialize(float damage)
        {
            Damage = damage;

            Destroy(gameObject, LifeTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"Entered {collision.gameObject.name}");

            if (collision.gameObject.TryGetComponent(out BodyPartDamageMultiplier mutantPart))
            {
                float calculatedDamage = mutantPart.GetCalculatedDamage(Damage);

                ApplyAttack(mutantPart.MutantHealth, calculatedDamage);

                Destroy(gameObject);
            }
        }
        
        public void ApplyAttack(IDamageable target, float calculatedDamage) => target.ApplyDamage(calculatedDamage);
    }
}
