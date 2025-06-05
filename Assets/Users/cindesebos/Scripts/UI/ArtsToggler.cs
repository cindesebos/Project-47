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

        private Sprite[] _sprites;
        private int _index;

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
            _sprites = null;

            OnShow?.Invoke();

            _artVisual.gameObject.SetActive(true);

            _artVisual.sprite = sprite;

            _character.AudioSource.PlayOneShot(_soundsContainer.InteractionWithArtSound);
        }

        public void Show(Sprite[] sprites)
        {
            if (_sprites != sprites)
            {
                _sprites = sprites;
                _index = 0;
            }
            else
            {
                _index++;

                if (_index >= _sprites.Length)
                {
                    _sprites = null;

                    _artVisual.gameObject.SetActive(false);

                    OnHide?.Invoke();

                    return;
                }
            }


            _input.Interaction.Hide.performed += Hide;

            OnShow?.Invoke();

            _artVisual.gameObject.SetActive(true);

            _artVisual.sprite = _sprites[_index];

            _character.AudioSource.PlayOneShot(_soundsContainer.InteractionWithArtSound);
        }

        public void Hide(InputAction.CallbackContext context)
        {
            if (_sprites != null)
            {
                Show(_sprites);

                return;
            }

            _artVisual.gameObject.SetActive(false);

            _input.Interaction.Hide.performed -= Hide;

            OnHide?.Invoke();
        }

        private void OnDestroy() => _input.Disable();
    }
}
