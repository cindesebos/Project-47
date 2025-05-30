<<<<<<< Updated upstream
using System;
=======
using UnityEngine;
using Scripts.Items;
>>>>>>> Stashed changes
using Scripts.Character.Inventory;
using Scripts.Items;
using Scripts.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

namespace Scripts.Character
{
    public class CharacterItemProvider : MonoBehaviour
    {
        private Transform _cameraOrigin;
        private float _rayDistance;
        private LayerMask _targetLayer;
        private IInventory _inventory;
        private ArtsToggler _artsToggler;

        private Item _currentSelectedItem;
<<<<<<< Updated upstream
        public Note _currentSelectedNote;
        private RaycastHit _lastHit;
        private bool _hasHit;

        public void Initialize(Transform cameraOrigin, CharacterData data, IInventory inventory, ArtsToggler artsToggler)
=======
        private RaycastHit _lastHit;
        private bool _hasHit;

        public void Initialize(Transform cameraOrigin, CharacterData data, IInventory inventory)
>>>>>>> Stashed changes
        {
            _cameraOrigin = cameraOrigin;
            _rayDistance = data.RayDistance;
            _targetLayer = data.TargetLayer;
            _inventory = inventory;
<<<<<<< Updated upstream
            _artsToggler = artsToggler;
=======
>>>>>>> Stashed changes
        }

        public void UseItem(InputAction.CallbackContext context)
        {
<<<<<<< Updated upstream
            if (_currentSelectedItem)
            {
                if (_inventory.TryAddItem(_currentSelectedItem.Data))
                {
                    Destroy(_currentSelectedItem.gameObject);

                    _currentSelectedItem = null;

                    return;
                }
            }

            if (_currentSelectedNote)
            {
                var control = context.control;

                _artsToggler.Show(_currentSelectedNote.Data.Sprite);

                _currentSelectedNote = null;
=======
            if (_currentSelectedItem == null) return;

            if (_inventory.TryAddItem(_currentSelectedItem.Data))
            {
                Destroy(_currentSelectedItem.gameObject);
                _currentSelectedItem = null;
>>>>>>> Stashed changes
            }
        }

        public void Handle()
        {
            if (_currentSelectedItem != null)
                _currentSelectedItem.SetOutlineVisible(false);
<<<<<<< Updated upstream

            if (_currentSelectedNote != null)
                _currentSelectedNote.SetOutlineVisible(false);

=======

            _currentSelectedItem = null;
>>>>>>> Stashed changes
            _hasHit = false;

            var ray = new Ray(_cameraOrigin.position, _cameraOrigin.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _targetLayer))
            {
<<<<<<< Updated upstream
                if (hit.collider.GetComponent<ISelectable>() != null)
                {
                    if (hit.collider.TryGetComponent(out Item item))
                    {
                        Debug.Log($"Item found: {item.Data.Name}");

                        _currentSelectedItem = item;
                        _currentSelectedItem.SetOutlineVisible(true);
                        _lastHit = hit;
                        _hasHit = true;
                    }

                    if (hit.collider.TryGetComponent(out Note note))
                    {
                        Debug.Log($"Note found: {note.Data}");

                        _currentSelectedNote = note;
                        _currentSelectedNote.SetOutlineVisible(true);

                        _lastHit = hit;
                        _hasHit = true;
                    }
                }
            }
            else
            {
                _currentSelectedItem = null;
                _currentSelectedNote = null;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_cameraOrigin == null)
                return;
=======
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
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream

        internal void Initialize(Transform transform, CharacterData data, IInventory inventory)
        {
            throw new NotImplementedException();
        }
=======
>>>>>>> Stashed changes
#endif
    }
}