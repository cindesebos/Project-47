using UnityEngine;
using Scripts.Items.Simply;
using Scripts.Items.Gun;
using Scripts.Menu;

namespace Scripts.Utils.Storage
{
    public class StorageService : IStorageService
    {
        private const string MedKitKey = "Save_MedKitAmount";
        private const string AmmoKey = "Save_AmmoAmount";
        private const string SettingsSensitivityKey = "Save_Settings_Sensitivity";
        private const string SettingsFpsToggleKey = "Save_Settings_FpsToggle";
        private const string SettingsFpsLimitToggleKey = "Save_Settings_FpsLimit";
        private const string SettingsMasterKey = "Save_Settings_Master";
        private const string SettingsMusicKey = "Save_Settings_Music";
        private const string SettingsSoundsKey = "Save_Settings_Sounds";

        private FirstAidKitLogic _firstAidKitLogic;
        private GunShooter _gunShooter;
        private SettingsHandler _settingsHandler;

        public void Initialize(FirstAidKitLogic firstAidKitLogic = null, Character.Character character = null, SettingsHandler settingsHandler = null)
        {
            _firstAidKitLogic = firstAidKitLogic;
            if (character != null) _gunShooter = character.GunShooter;
            _settingsHandler = settingsHandler;
        }

        public void Save()
        {
            if (_firstAidKitLogic != null) PlayerPrefs.SetFloat(MedKitKey, _firstAidKitLogic.Amount);
            if (_gunShooter != null) PlayerPrefs.SetInt(AmmoKey, _gunShooter.CurrentAmmoAmount);
            if (_settingsHandler != null)
            {
                PlayerPrefs.SetFloat(SettingsSensitivityKey, _settingsHandler.SensitivityValue);
                PlayerPrefs.SetInt(SettingsFpsToggleKey, _settingsHandler.FpsDisplayerToggleState ? 1 : 0);
                PlayerPrefs.SetInt(SettingsFpsLimitToggleKey, _settingsHandler.FpsLimitValue);
                PlayerPrefs.SetFloat(SettingsMasterKey, _settingsHandler.MasterValue);
                PlayerPrefs.SetFloat(SettingsMusicKey, _settingsHandler.MusicValue);
                PlayerPrefs.SetFloat(SettingsSoundsKey, _settingsHandler.SoundsValue);

                Debug.Log($"Loaded savedSensitivityValue: {_settingsHandler.SensitivityValue }");
            }
            PlayerPrefs.Save();
        }

        public void Load()
        {
            int savedMedKits = PlayerPrefs.GetInt(MedKitKey, 0);
            int savedAmmo = PlayerPrefs.GetInt(AmmoKey, 0);
            float savedSensitivityValue = PlayerPrefs.GetFloat(SettingsSensitivityKey, 0.05f);
            bool savedFpsToggle = PlayerPrefs.GetInt(SettingsFpsToggleKey, 0) == 1;
            int savedFpsLimit = PlayerPrefs.GetInt(SettingsFpsLimitToggleKey, 0);
            float savedMasterValue = PlayerPrefs.GetFloat(SettingsMasterKey, 0f);
            float savedMusicValue = PlayerPrefs.GetFloat(SettingsMusicKey, 0f);
            float savedSoundsValue = PlayerPrefs.GetFloat(SettingsSoundsKey, 0f);

            if (_firstAidKitLogic != null)
            {
                _firstAidKitLogic.SetAmount(savedMedKits);
            }

            if (_gunShooter != null)
            {
                _gunShooter.SetAmmo(savedAmmo);
            }

            if (_settingsHandler != null)
            {
                _settingsHandler.Load(savedFpsToggle, savedFpsLimit, savedMasterValue, savedMusicValue, savedSoundsValue, savedSensitivityValue);

                Debug.Log($"Loaded savedSensitivityValue: {savedSensitivityValue}");
            }
        }

        public void Reset()
        {
            PlayerPrefs.DeleteKey(MedKitKey);
            PlayerPrefs.DeleteKey(AmmoKey);
            PlayerPrefs.Save();

            _firstAidKitLogic.SetAmount(0);
            if(_gunShooter != null) _gunShooter.SetAmmo(0);

            Debug.Log("Storage reset: MedKits and Ammo set to 0.");
        }
    }
}