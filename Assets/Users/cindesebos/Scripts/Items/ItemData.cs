using UnityEngine;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "Item Data", menuName = "Datas/Items/New Item Data")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Id { get; private set; }
        [field: Space(10)]

        [field: SerializeField] public float OutlineWidth { get; private set; }
    }
}
