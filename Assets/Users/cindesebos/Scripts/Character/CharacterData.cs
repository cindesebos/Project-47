using UnityEngine;

namespace Scripts.Character
{
    [CreateAssetMenu(fileName = "Character Data", menuName = "Datas/New Character Data")]
    public class CharacterData : ScriptableObject
    {
        [field: SerializeField] public float WalkSpeed { get; private set; } = 5f;
        [field: SerializeField] public float RunSpeed { get; private set; } = 9f;
        [field: SerializeField] public float MouseSensativity { get; private set; } = 2f;
        [field: SerializeField] public float RayAreaRadius { get; private set; } = 1.5f;
        [field: SerializeField] public float BobSpeed { get; private set; } = 10f;
        [field: SerializeField] public Vector3 BobAmount { get; private set; } = new Vector3(0.15f, 0.05f, 0f);
        [field: SerializeField] public float Gravity { get; private set; } = -9.81f;
        [field: SerializeField] public float MinPitch { get; private set; } = -80f;
        [field: SerializeField] public float MaxPitch { get; private set; } = 80f;
    }
}
