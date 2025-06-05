using Scripts.Menu;
using Scripts.Utils.Storage;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private SettingsHandler _settingsHandler;

        public override void InstallBindings()
        {
            BindSettingsHandler();
            BindStorageService();
        }

        private void BindSettingsHandler()
        {
            Container.Bind<SettingsHandler>()
                .FromInstance(_settingsHandler)
                .AsSingle();
        }

        private void BindStorageService()
        {
            Container.Bind<IStorageService>()
                .To<StorageService>()
                .AsSingle()
                .NonLazy();
        }
    }
}
