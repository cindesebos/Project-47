using UnityEngine;
using Scripts.Items;
using Scripts.Character.Inventory;
using UnityEngine.InputSystem;

namespace Scripts.Character
{
    public class CharacterItemProvider : MonoBehaviour
    {
        private Transform _cameraOrigin;
        private float _rayDistance;
        private LayerMask _targetLayer;
        private IInventory _inventory;

        private Item _currentSelectedItem;
        private RaycastHit _lastHit;
        private bool _hasHit;

        public void Initialize(Transform cameraOrigin, CharacterData data, IInventory inventory)
        {
            _cameraOrigin = cameraOrigin;
            _rayDistance = data.RayDistance;
            _targetLayer = data.TargetLayer;
            _inventory = inventory;
        }

        public void UseItem(InputAction.CallbackContext context)
        {
            if (_currentSelectedItem == null) return;

            if (_inventory.TryAddItem(_currentSelectedItem.Data))
            {
                Destroy(_currentSelectedItem.gameObject);
                _currentSelectedItem = null;
            }
        }

        public void Handle()
        {
            if (_currentSelectedItem != null)
                _currentSelectedItem.SetOutlineVisible(false);

            _currentSelectedItem = null;
            _hasHit = false;

            var ray = new Ray(_cameraOrigin.position, _cameraOrigin.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _targetLayer))
            {
                Debug.Log($"Hit: {hit.collider.name} at {hit.point}");

                if (hit.collider.TryGetComponent(out Item item))
                {
                    Debug.Log($"Item found: {item.Data.Name}");

                    _currentSelectedItem = item;
                    _currentSelectedItem.SetOutlineVisible(true);
                    _lastHit = hit;
                    _hasHit = true;
                }
            }
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_cameraOrigin == null) return;

            Gizmos.color = Color.cyan;
            Vector3 rayStart = _cameraOrigin.position;
            Vector3 rayEnd = rayStart + _cameraOrigin.forward * _rayDistance;

            Gizmos.DrawLine(rayStart, rayEnd);

            if (_hasHit)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(_lastHit.point, 0.1f);
            }
        }
#endif
    }
}