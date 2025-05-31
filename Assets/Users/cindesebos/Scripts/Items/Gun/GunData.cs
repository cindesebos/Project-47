using UnityEngine;

namespace Scripts.Items.Gun
{
    [CreateAssetMenu(fileName = "Gun Data", menuName = "Datas/New Gun Data")]
    public class GunData : ScriptableObject
    {
        [field: SerializeField] public float Range { get; private set; } = 100f;
        [field: SerializeField] public float Damage { get; private set; } = 10f;
        [field: SerializeField] public float BulletSpeed { get; private set; } = 8f;
    }
}