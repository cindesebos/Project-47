using UnityEngine;
using Zenject;

namespace Scripts.Mutant
{
    public class MutantView : MonoBehaviour
    {
        private const string AttackKey = "attack";

        private Animator _animator;
        private MutantAttacker _attacker;

        public void Initialize(Animator animator, MutantAttacker attacker)
        {
            _animator = animator;

            _attacker = attacker;

            _attacker.OnAttackStarted += OnAttack;
        }

        private void OnAttack()
        {
            _animator.SetTrigger(AttackKey);
        }

        private void OnDestroy()
        {
            _attacker.OnAttackStarted -= OnAttack;
        }
    }
}