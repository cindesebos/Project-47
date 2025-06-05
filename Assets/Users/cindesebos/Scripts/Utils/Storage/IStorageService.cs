using Scripts.Items.Simply;
using Scripts.Menu;
using UnityEngine;

namespace Scripts.Utils.Storage
{
    public interface IStorageService
    {
        void Initialize(FirstAidKitLogic firstAidKitLogic = null, Character.Character character = null, SettingsHandler settingsHandler = null);
        void Save();
        void Load();
        void Reset();
    }
}