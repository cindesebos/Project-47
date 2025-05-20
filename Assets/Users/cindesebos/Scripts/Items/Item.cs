using UnityEngine;

namespace Scripts.Items
{
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ItemData Data { get; private set; }
    }
}
