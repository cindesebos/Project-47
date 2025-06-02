using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using Scripts.Utils;
using Zenject;
using Scripts.Sounds;

namespace Scripts.UI
{
    public class ArtsToggler : MonoBehaviour
    {
        public event Action OnShow;
        public event Action OnHide;

        [SerializeField] private Image _artVisual;

        private UIInput _input;
        private Character.Character _character;
        private SoundsContainer _soundsContainer;

        [Inject]
        private void Construct(UIInput input, Character.Character character, SoundsContainer soundsContainer)
        {
            _input = input;
            _character = character;
            _soundsContainer = soundsContainer;

            _input.Enable();

            Debug.Log($"Input: {_input}");
        }

        public void Show(Sprite sprite)
        {
            _input.Interaction.Hide.performed += Hide;

            OnShow?.Invoke();

            _artVisual.gameObject.SetActive(true);

            _artVisual.sprite = sprite;

            _character.AudioSource.PlayOneShot(_soundsContainer.InteractionWithArtSound);
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
