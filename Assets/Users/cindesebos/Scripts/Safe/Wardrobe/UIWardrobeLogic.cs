using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Zenject;
using Scripts.UI;
using Scripts.Sounds;

namespace Scripts.Safe.Wardrobe
{
    public class UIWardrobeLogic : MonoBehaviour
    {
        public event Action OnWardrobeOpened;

        [SerializeField] private int[] _targetCode = { 6, 9, 4 };
        [SerializeField] private GameObject _view;
        [SerializeField] private LockPinRotator[] _lockPinsRotator;

        [SerializeField] private int[] _currentCode = new int[3];
        [SerializeField] private AudioSource _audioSource;

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

        public void OnOpen()
        {
            foreach (var lockPinRotator in _lockPinsRotator) lockPinRotator.Initialize(this);

            _cursorHandler.SetVisibility(true);
            _character.DisableInput();

            _view.SetActive(true);
        }

        public void UpdateCurrentCode(int index, int value)
        {
            _currentCode[index] = value;

            _audioSource.PlayOneShot(_soundsContainer.RotatingLockPinSound);

            if (IsCodeCorrect()) Unlock();
        }

        private bool IsCodeCorrect()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_currentCode[i] != _targetCode[i]) return false;
            }

            return true;
        }

        private void Unlock()
        {
            Close();

            _audioSource.PlayOneShot(_soundsContainer.InputCurrentCodeSound);

            OnWardrobeOpened?.Invoke();
        }

        public void Close()
        {
            _cursorHandler.SetVisibility(false);
            _character.EnableInput();

            _view.SetActive(false);
        }
    }
}
