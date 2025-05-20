using UnityEngine;

namespace Scripts.UI
{
    public class CursorHandler : MonoBehaviour
    {
        [SerializeField] private bool _state = false;

        private void Start()
        {
            SetCursorState(_state);
        }

        public void SetCursorState(bool visible)
        {
            Cursor.visible = visible;
            Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public void ToggleCursor()
        {
            _state = !_state;

            SetCursorState(_state);
        }
    }
}
