using UnityEngine;

namespace Scripts.Items
{
    [RequireComponent(typeof(Outline), typeof(BoxCollider))]
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ItemData Data { get; private set; }

        private Outline _outline;

        private void Awake()
        {
            _outline = GetComponent<Outline>();

            _outline.OutlineWidth = 0f;
        }

        public void SetOutlineVisible(bool visible) => _outline.OutlineWidth = visible ? Data.OutlineWidth : 0;
    }
}
