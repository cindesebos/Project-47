using UnityEngine;

namespace Scripts.Items
{
    [RequireComponent(typeof(Outline), typeof(BoxCollider))]
    public class Item : MonoBehaviour, ISelectable
    {
        private const string DefaultLayerName = "Item";

        [field: SerializeField] public ItemData Data { get; private set; }

        private Outline _outline;

        private void Awake()
        {
            SetDefaultLayer();

            _outline = GetComponent<Outline>();

            _outline.OutlineWidth = 0f;
        }

        public void SetOutlineVisible(bool visible) => _outline.OutlineWidth = visible ? Data.OutlineWidth : 0;

        private void SetDefaultLayer() => gameObject.layer = LayerMask.NameToLayer(DefaultLayerName);
    }
}
