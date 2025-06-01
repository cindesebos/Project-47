using UnityEngine;

namespace Scripts.Mutant
{
    [CreateAssetMenu(fileName = "Mutant Data", menuName = "Datas/New Mutant Data")]
    public class MutantData : ScriptableObject
    {
        [field: SerializeField] public float WalkSpeed { get; private set; } = 5f;
        [field: SerializeField] public float Health { get; private set; } = 50f;

        [field: SerializeField] public float HeadDamageMultiplier { get; private set; } = 3f;
        [field: SerializeField] public float BodyDamageMultiplier { get; private set; } = 1f;
        [field: SerializeField] public float ArmsDamageMultiplier { get; private set; } = 0.5f;
        [field: SerializeField] public float LegsDamageMultiplier { get; private set; } = 0.5f;

        [field: SerializeField] public float StoppingDistance { get; private set; } = 0.05f;

        [field: SerializeField] public float VisionRange { get; private set; } = 20f;
        [field: SerializeField] public float VisionAngle { get; private set; } = 60f;
        [field: SerializeField] public float MaxLostTargetTime { get; private set; } = 4f;
        [field: SerializeField] public LayerMask CharacterLayer { get; private set; }

        [field: SerializeField] public Color GizmoColor { get; private set; } = Color.red;
        [field: SerializeField] public float GizmoRadius { get; private set; } = 0.05f;
    }
}