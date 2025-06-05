using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Safe.Wardrobe
{
    public class LockPinRotator : MonoBehaviour
    {
        [SerializeField] private int _index;
        [SerializeField] private int _currentValue = 0;
        [Space]

        [SerializeField] private float _rotationStep = 40f;

        [SerializeField] private UIWardrobeLogic _uiWardrobeLogic;


        public void Initialize(UIWardrobeLogic uiWardrobeLogic)
        {
            _uiWardrobeLogic = uiWardrobeLogic;

            _uiWardrobeLogic.UpdateCurrentCode(_index, _currentValue);
        }

        private void OnMouseDown()
        {
            Debug.Log("Pinning");

            Rotate();
        }

        public void Rotate()
        {
            _currentValue++;
            
            if (_currentValue > 9) _currentValue = 1;

            transform.Rotate(Vector3.right, -_rotationStep);
            _uiWardrobeLogic.UpdateCurrentCode(_index, _currentValue);
        }
    }
}