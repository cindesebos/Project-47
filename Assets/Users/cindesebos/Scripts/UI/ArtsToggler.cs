using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using Zenject;

namespace Scripts.UI
{
    public class ArtsToggler : MonoBehaviour
    {
        public event Action OnShow;
        public event Action OnHide;

        [SerializeField] private Image _artVisual;

        private UIInput _input;

        [Inject]
        private void Construct(UIInput input)
        {
            _input = input;

            _input.Enable();

            Debug.Log($"Input: {_input}");
        }

        public void Show(Sprite sprite)
        {
            _input.Interaction.Hide.performed += Hide;

            OnShow?.Invoke();

            _artVisual.gameObject.SetActive(true);

            _artVisual.sprite = sprite;
        }

        public void Hide(InputAction.CallbackContext context)
        {
            _artVisual.gameObject.SetActive(false);

            _input.Interaction.Hide.performed -= Hide;

            OnHide?.Invoke();
        }

        private void OnDestroy() => _input.Disable();
    }
}
