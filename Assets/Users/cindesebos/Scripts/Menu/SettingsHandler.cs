using Scripts.UI.FPS;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Menu
{
    public class SettingsHandler : MonoBehaviour
    {
        private const string MasterKey = "Master";
        private const string MusicKey = "Music";
        private const string SoundsKey = "Sounds";

        public bool FpsDisplayerToggleState { get; set; }
        public int FpsLimitValue { get; set; }
        public float MasterValue { get; set; }
        public float MusicValue { get; set; }
        public float SoundsValue { get; set; }

        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _soundsSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Toggle _fpsDisplayerToggler;
        [SerializeField] private TMP_Dropdown _fpsLimitDropdown;
        [Space]

        [SerializeField] private AudioMixer _audioMixer;

        private FPSDisplayer _fpsDisplayer;

        [Inject]
        private void Construct(FPSDisplayer fpsDisplayer)
        {
            _fpsDisplayer = fpsDisplayer;
        }

        private void Start()
        {
            _fpsDisplayerToggler.onValueChanged.AddListener(OnFpsTogglerValueChanged);
            _fpsLimitDropdown.onValueChanged.AddListener(OnFpsLimitValueChanged);

            _masterSlider.onValueChanged.AddListener(OnMasterValueChanged);
            _soundsSlider.onValueChanged.AddListener(OnSoundsValueChanged);
            _musicSlider.onValueChanged.AddListener(OnMusicValueChanged);
        }

        public void OnMasterValueChanged(float value)
        {
            MasterValue = value;

            _audioMixer.SetFloat(MasterKey, MasterValue);
        }

        public void OnSoundsValueChanged(float value)
        {
            SoundsValue = value;

            _audioMixer.SetFloat(SoundsKey, SoundsValue);
        }

        public void OnMusicValueChanged(float value)
        {
            MusicValue = value;

            _audioMixer.SetFloat(MusicKey, MusicValue);
        }

        public void Load(bool fpsDisplayerState, int fpsLimitValue, float masterValue, float musicValue, float soundsValue)
        {
            _fpsDisplayerToggler.isOn = fpsDisplayerState;
            OnFpsTogglerValueChanged(fpsDisplayerState);

            _fpsLimitDropdown.value = fpsLimitValue;
            OnFpsLimitValueChanged(fpsLimitValue);

            _masterSlider.value = masterValue;
            OnMasterValueChanged(masterValue);

            _musicSlider.value = musicValue;
            OnMusicValueChanged(musicValue);

            _soundsSlider.value = soundsValue;
            OnSoundsValueChanged(soundsValue);

            Debug.Log("Settings Handler was loaded");
        }

        private void OnFpsTogglerValueChanged(bool state)
        {
            FpsDisplayerToggleState = state;

            _fpsDisplayer.Toggle(FpsDisplayerToggleState);
        }

        private void OnFpsLimitValueChanged(int value)
        {
            QualitySettings.vSyncCount = 0;

            FpsLimitValue = value;

            switch (value)
            {
                case 0: Application.targetFrameRate = -1; break;
                case 1: Application.targetFrameRate = 30; break;
                case 2: Application.targetFrameRate = 60; break;
                case 3: Application.targetFrameRate = 120; break;
                case 4: Application.targetFrameRate = 144; break;
                case 5: Application.targetFrameRate = 240; break;
            }
        }

        private void OnDestroy()
        {
            _fpsDisplayerToggler.onValueChanged.RemoveListener(OnFpsTogglerValueChanged);
            _fpsLimitDropdown.onValueChanged.RemoveListener(OnFpsLimitValueChanged);

            _masterSlider.onValueChanged.RemoveListener(OnMasterValueChanged);
            _soundsSlider.onValueChanged.RemoveListener(OnSoundsValueChanged);
            _musicSlider.onValueChanged.RemoveListener(OnMusicValueChanged);
        }
    }
}
