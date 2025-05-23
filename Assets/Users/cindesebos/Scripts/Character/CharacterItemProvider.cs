using UnityEngine;
using System.Collections.Generic;
using Scripts.Items;
using Scripts.Character.Inventory;
using UnityEngine.InputSystem;

namespace Scripts.Character
{
    public class CharacterItemProvider : MonoBehaviour
    {
        private Transform _cameraOrigin;
        private float _rayAreaRadius;
        private IInventory _inventory;

        public List<Item> _selectedItems = new();

        public Item _currentSelectedItem;
        private SphereCollider _sphereCollider;

        public void Initialize(Transform cameraOrigin, CharacterData data, IInventory inventory)
        {
            _cameraOrigin = cameraOrigin;
            _rayAreaRadius = data.RayAreaRadius;
            _inventory = inventory;

            _sphereCollider = GetComponent<SphereCollider>();
            _sphereCollider.radius = _rayAreaRadius;
        }

        public void UseItem(InputAction.CallbackContext context)
        {
            if (_currentSelectedItem == null) return;
            
            if (_inventory.TryAddItem(_currentSelectedItem.Data))
            {
                
                Item item = _currentSelectedItem;

                _selectedItems.Remove(item);

                Destroy(item.gameObject);
            }
        }

        public void Handle()
        {
            _currentSelectedItem = null;

            if (_selectedItems.Count == 0) return;

            float bestDot = 0f;

            var ray = new Ray(_cameraOrigin.position, _cameraOrigin.forward);

            foreach (var item in _selectedItems)
            {
                if (item == null) return;

                item.SetOutlineVisible(false);

                Vector3 toTarget = item.transform.position - ray.origin;

                float dot = Vector3.Dot(ray.direction.normalized, toTarget.normalized);

                if (dot > bestDot)
                {
                    bestDot = dot;

                    _currentSelectedItem = item;
                }
            }

            _currentSelectedItem?.SetOutlineVisible(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Item item) && item.enabled) _selectedItems.Add(item);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Item item))
            {
                _selectedItems.Remove(item);
                item.SetOutlineVisible(false);
            }
        }
    }
}
