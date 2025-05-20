using UnityEngine;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "Item Data", menuName = "Datas/New Item Data")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Id { get; private set; }
    }
}
