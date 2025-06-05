using System;
using Scripts.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Scripts.Pause
{
    public class PauseHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _view;

        private Character.Character _character;
        private UIInput _input;
        private CursorHandler _cursorHandler;

        [Inject]
        private void Construct(Character.Character character, UIInput input, CursorHandler cursorHandler)
        {
            _character = character;
            _input = input;
            _cursorHandler = cursorHandler;
        }

        private void Start()
        {
            Debug.Log(_input);

            _input.Interaction.Pause.performed += Handle;
        }

        private void Handle(InputAction.CallbackContext context)
        {
            Debug.Log(_view.activeInHierarchy);

            if (_view.activeInHierarchy) Hide();
            else Show();
        }

        private void Show()
        {
            _cursorHandler.SetVisibility(true);

            _view.SetActive(true);

            _character.DisableInput();
        }

        public void Hide()
        {
            _cursorHandler.SetVisibility(false);
            
            _view.SetActive(false);

            _character.EnableInput();
        }
        
        private void OnDestroy()
        {
            _input.Interaction.Pause.performed -= Handle;   
        }
    }
}
