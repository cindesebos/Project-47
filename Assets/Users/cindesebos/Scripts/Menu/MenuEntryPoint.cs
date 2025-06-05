using Scripts.Sounds;
using Scripts.UI;
using Scripts.Utils.Storage;
using UnityEngine;
using Zenject;

namespace Scripts.Menu
{
    public class MenuEntryPoint : MonoBehaviour
    {
        private IStorageService _storageService;
        private CursorHandler _cursorHandler;
        private BackGroundMusicPlayer _backGroundMusicPlayer;
        private SettingsHandler _settingsHandler;

        [Inject]
        private void Construct(IStorageService storageService, CursorHandler cursorHandler,
        BackGroundMusicPlayer backGroundMusicPlayer, SettingsHandler settingsHandler)
        {
            _cursorHandler = cursorHandler;
            _backGroundMusicPlayer = backGroundMusicPlayer;
            _settingsHandler = settingsHandler;
            _storageService = storageService;
        }

        private void Start()
        {
            _storageService.Initialize(null, null, _settingsHandler);

            _storageService.Load();

            _cursorHandler.SetVisibility(true);
            _backGroundMusicPlayer.PlayMenuMusic();
        }

        private void OnDisable()
        {
            _storageService.Save();
        }
    }
}
