using UnityEngine;
using Scripts.Items.Simply;
using Scripts.Items.Gun;

namespace Scripts.Utils.Storage
{
    public class StorageService : IStorageService
    {
        private const string MedKitKey = "Save_MedKitAmount";
        private const string AmmoKey = "Save_AmmoAmount";

        private readonly FirstAidKitLogic _firstAidKitLogic;
        private readonly GunShooter _gunShooter;

        public StorageService(FirstAidKitLogic firstAidKitLogic, Character.Character character)
        {
            _firstAidKitLogic = firstAidKitLogic;
            _gunShooter = character.GunShooter;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(MedKitKey, _firstAidKitLogic.Amount);
            PlayerPrefs.SetInt(AmmoKey, _gunShooter.CurrentAmmoAmount);
            PlayerPrefs.Save();

            Debug.Log($"Saved MedKits: {_firstAidKitLogic.Amount}, Ammo: {_gunShooter.CurrentAmmoAmount}");
        }

        public void Load()
        {
            int savedMedKits = PlayerPrefs.GetInt(MedKitKey, 0);
            int savedAmmo = PlayerPrefs.GetInt(AmmoKey, 0);

            _firstAidKitLogic.SetAmount(savedMedKits);

            Debug.Log($"GunShooter {_gunShooter.name} loaded ammo: {savedAmmo}");
            
            _gunShooter.SetAmmo(savedAmmo);

            Debug.Log($"Loaded MedKits: {savedMedKits}, Ammo: {savedAmmo}");
        }

        public void Reset()
        {
            PlayerPrefs.DeleteKey(MedKitKey);
            PlayerPrefs.DeleteKey(AmmoKey);
            PlayerPrefs.Save();

            _firstAidKitLogic.SetAmount(0);
            _gunShooter.SetAmmo(0);

            Debug.Log("Storage reset: MedKits and Ammo set to 0.");
        }
    }
}