using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Zenject;
using Scripts.UI;

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

        private string _currentInputValue = "";

        private CursorHandler _cursorHandler;

        [Inject]
        private void Construct(CursorHandler cursorHandler)
        {
            _cursorHandler = cursorHandler;
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

            _view.SetActive(true);

            OnClearButtonClicked();

            UpdateTextDisplay();
        }

        public void InitializeDigitButtons()
        {
            for (int i = 0; i < _digitButtons.Length; i++)
            {
                int digit = i;

                _digitButtons[i].onClick.AddListener(() => OnDigitButtonClicked(digit));
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

                    Close();
                }
                else
                {
                    Debug.LogWarning("Input is incorrect.");

                    OnClearButtonClicked();

                    UpdateTextDisplay(ErrorText);
                }
            });
        }

        private void OnClearButtonClicked()
        {
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

            _view.SetActive(false);
        }

        private void UpdateTextDisplay() => _displayText.text = _currentInputValue;

        private void UpdateTextDisplay(string text) => _displayText.text = text;
    }
}
