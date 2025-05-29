using UnityEngine;

namespace Scripts.UI
{
    public class CursorHandler : MonoBehaviour
    {
        [SerializeField] private bool _isVisible = false;
        [SerializeField] private GameObject _cursorObject;

        private void Start() => SetVisibility(_isVisible);

        public void SetVisibility(bool visible)
        {
            _isVisible = visible;

            Cursor.visible = visible;
            Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public void SetLockState(bool state) => Cursor.lockState = state ? CursorLockMode.Locked : CursorLockMode.None;

        public void ToggleVisibility()
        {
            _isVisible = !_isVisible;

            SetVisibility(_isVisible);
        }
    }
}
