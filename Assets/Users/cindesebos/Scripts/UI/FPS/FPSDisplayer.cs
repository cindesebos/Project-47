using TMPro;
using UnityEngine;

namespace Scripts.UI.FPS
{
    public class FPSDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _updateInterval = 0.5f;

        private float _accumulatedTime;
        private int _frames;
        private float _timeLeft;

        private void Start() => _timeLeft = _updateInterval;

        public void Toggle(bool visible) => _text.gameObject.SetActive(visible);

        private void Update()
        {
            _timeLeft -= Time.deltaTime;

            _accumulatedTime += 1f / Time.deltaTime;

            _frames++;

            if (_timeLeft <= 0f)
            {
                float fps = _accumulatedTime / _frames;

                _text.text = $"FPS: {Mathf.RoundToInt(fps)}";

                _timeLeft = _updateInterval;

                _accumulatedTime = 0f;

                _frames = 0;
            }
        }
    }
}
