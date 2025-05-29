using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "Item Data", menuName = "Datas/Items/New Note Data")]
    public class NoteData : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public float OutlineWidth { get; private set; } = 5f;
    }
}
