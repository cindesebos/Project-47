using UnityEngine;

namespace Scripts.Items.Types
{
    [CreateAssetMenu(fileName = "Interactable Item Data", menuName = "Datas/Items/New Interactable Item Data")]
    public class InteractableItemData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int TargetItemId { get; private set; }
        [field: Space(10)]

        [field: SerializeField] public float OutlineWidth { get; private set; }
    }
}
