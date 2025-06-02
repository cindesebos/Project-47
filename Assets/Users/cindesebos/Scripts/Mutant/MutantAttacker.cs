using System;
using UnityEngine;

namespace Scripts.Mutant
{
    [RequireComponent(typeof(SphereCollider))]
    public class MutantAttacker : MonoBehaviour
    {
        public event Action OnAttackStarted;
        public event Action OnAttackFinished;

        [SerializeField] private Transform _attackOrigin;

        private float _damage;
        private float _detectionRadius;
        private float _attackRadius;
        private MutantData _data;
        private bool _alreadyAttacked;

        private SphereCollider _detectionCollider;
        private Transform _target;

        public void Initialize(MutantData data)
        {
            _data = data;
            _damage = data.Damage;
            _attackRadius = data.AttackRadius;
            _detectionRadius = data.DetectionRadius;

            _detectionCollider = GetComponent<SphereCollider>();
            _detectionCollider.isTrigger = true;
            _detectionCollider.radius = _detectionRadius;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_alreadyAttacked && other.gameObject.GetComponent<Character.Character>())
            {
                _target = other.transform;

                LookAtTarget();

                _alreadyAttacked = true;
                Debug.Log("Preparing attack on character");

                OnAttackStarted?.Invoke();
            }
        }

        public void Attack()
        {
            Vector3 origin = _attackOrigin != null ? _attackOrigin.position : transform.position;

            Collider[] hits = Physics.OverlapSphere(origin, _attackRadius);

            foreach (var hit in hits)
            {
                var character = hit.GetComponent<Character.CharacterHealth>();

                if (character != null)
                {
                    character.ApplyDamage(_damage);
                    Debug.Log($"Attacked {character.name} for {_damage} damage.");
                }
            }
        }

        private void LookAtTarget()
        {
            if (_target == null) return;

            Vector3 direction = (_target.position - transform.position).normalized;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = lookRotation;
            }
        }

        public void FinishAttack()
        {
            OnAttackFinished?.Invoke();
            _alreadyAttacked = false;
            _target = null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector3 origin = _attackOrigin != null ? _attackOrigin.position : transform.position;
            Gizmos.DrawWireSphere(origin, _attackRadius > 0 ? _attackRadius : 1f);
        }
    }
}