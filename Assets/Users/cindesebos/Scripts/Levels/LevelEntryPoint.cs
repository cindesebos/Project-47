using UnityEngine;
using Scripts.Utils.Storage;
using Zenject;
using Scripts.UI;
using Scripts.Sounds;

namespace Scripts.Levels
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [SerializeField] private bool _needToResetStorage = false;
        [SerializeField] private bool _needToStartGameplayMusic = false;

        private IStorageService _storageService;
        private CursorHandler _cursorHandler;
        private BackGroundMusicPlayer _backGroundMusicPlayer;

        [Inject]
        private void Construct(IStorageService storageService, CursorHandler cursorHandler, BackGroundMusicPlayer backGroundMusicPlayer)
        {
            _storageService = storageService;
            _cursorHandler = cursorHandler;
            _backGroundMusicPlayer = backGroundMusicPlayer;
        }

        private void Start()
        {
            if (_needToResetStorage) _storageService.Reset();

            _storageService.Load();
            _cursorHandler.SetVisibility(false);
            if(_needToStartGameplayMusic) _backGroundMusicPlayer.PlayGameplayMusic();
        }

        private void OnDisable()
        {
            _storageService.Save();
        }
    }
}
