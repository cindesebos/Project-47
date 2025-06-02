using UnityEngine;

namespace Scripts.Mutant
{
    public class BodyPartDamageMultiplier : MonoBehaviour
    {
        public IDamageable MutantHealth { get; private set; }

        private float _damageMultiplier;
        private Color _gizmoColor;
        private float _gizmoRadius;

        private bool _isInialized;

        public void Initialize(float damageMultiplier, MutantData mutantData, IDamageable mutantHealth)
        {
            _isInialized = true;

            _damageMultiplier = damageMultiplier;

            _gizmoColor = mutantData.GizmoColor;
            _gizmoRadius = mutantData.GizmoRadius;

            MutantHealth = mutantHealth;
        }

        public float GetCalculatedDamage(float damage) => damage * _damageMultiplier;

        private void OnDrawGizmos()
        {
            if (!_isInialized) return;

            Gizmos.color = _gizmoColor;

            Gizmos.DrawSphere(transform.position, _gizmoRadius);
#if UNITY_EDITOR
            UnityEditor.Handles.color = _gizmoColor;
            UnityEditor.Handles.Label(transform.position + Vector3.up * 0.15f, $"{name}\n×{_damageMultiplier}");
#endif
        }
    }
}
