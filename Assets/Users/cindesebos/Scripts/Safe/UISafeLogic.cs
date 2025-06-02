using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Zenject;
using Scripts.UI;
using Scripts.Sounds;

namespace Scripts.Safe
{
    public class UISafeLogic : MonoBehaviour
    {
        public event Action OnSafeOpened;

        private const int MaxNumberOfDigits = 4;
        private const string TargetInputValue = "2804";
        private const string ErrorText = "Error";

        private int _currentInputIndex = 0;

        [SerializeField] private GameObject _view;

        [SerializeField] private Button[] _digitButtons;
        [SerializeField] private Button _clearButton;
        [SerializeField] private Button _summitButton;
        [SerializeField] private TextMeshProUGUI _displayText;
        [SerializeField] private AudioSource _audioSource;

        private string _currentInputValue = "";

        private CursorHandler _cursorHandler;
        private Character.Character _character;
        private SoundsContainer _soundsContainer;

        [Inject]
        private void Construct(CursorHandler cursorHandler, Character.Character character, SoundsContainer soundsContainer)
        {
            _cursorHandler = cursorHandler;
            _character = character;
            _soundsContainer = soundsContainer;
        }

        private void Start()
        {
            InitializeDigitButtons();
            InitializeClearButton();
            InitializeSummitButton();

            Close();
        }

        public void OnOpen()
        {
            _cursorHandler.SetVisibility(true);
            _character.DisableInput();

            _view.SetActive(true);

            OnClearButtonClicked();

            UpdateTextDisplay();
        }

        public void InitializeDigitButtons()
        {
            for (int i = 0; i < _digitButtons.Length; i++)
            {
                int digit = i;

                _digitButtons[i].onClick.AddListener(() =>
                {
                    OnDigitButtonClicked(digit);
                    _audioSource.PlayOneShot(_soundsContainer.InputingSafeInputSound);
                });
            }
        }

        public void InitializeClearButton()
        {
            _clearButton.onClick.AddListener(OnClearButtonClicked);
        }

        public void InitializeSummitButton()
        {
            _summitButton.onClick.AddListener(() =>
            {
                if (_currentInputValue == TargetInputValue)
                {
                    Debug.Log("Input is correct.");

                    OnSafeOpened?.Invoke();

                    _audioSource.PlayOneShot(_soundsContainer.InputedCorrectSafeCodeSound);

                    Close();
                }
                else
                {
                    Debug.LogWarning("Input is incorrect.");

                    _audioSource.PlayOneShot(_soundsContainer.InputedIncorrectSafeCodeSound);

                    OnClearButtonClicked();

                    UpdateTextDisplay(ErrorText);
                }
            });
        }

        private void OnClearButtonClicked()
        {
            _audioSource.PlayOneShot(_soundsContainer.InputingSafeInputSound);
            
            _currentInputValue = "";

            _currentInputIndex = 0;

            UpdateTextDisplay();
        }

        private void OnDigitButtonClicked(int number)
        {
            if (_currentInputIndex >= MaxNumberOfDigits)
            {
                Debug.LogWarning("Maximum number of digits reached.");

                return;
            }

            _currentInputIndex++;

            _currentInputValue += number.ToString();

            Debug.Log($"Digit {number} clicked");

            UpdateTextDisplay();
        }

        public void Close()
        {
            _cursorHandler.SetVisibility(false);
            _character.EnableInput();

            _view.SetActive(false);
        }

        private void UpdateTextDisplay() => _displayText.text = _currentInputValue;

        private void UpdateTextDisplay(string text) => _displayText.text = text;
    }
}
