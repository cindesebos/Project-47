using Scripts.Sounds;
using Scripts.UI;
using UnityEngine;
using Zenject;

namespace Scripts.Menu
{
    public class MenuEntryPoint : MonoBehaviour
    {
        private CursorHandler _cursorHandler;
        private BackGroundMusicPlayer _backGroundMusicPlayer;

        [Inject]
        private void Construct(CursorHandler cursorHandler, BackGroundMusicPlayer backGroundMusicPlayer)
        {
            _cursorHandler = cursorHandler;
            _backGroundMusicPlayer = backGroundMusicPlayer;
        }

        private void Start()
        {
            _cursorHandler.SetVisibility(true);
            _backGroundMusicPlayer.PlayMenuMusic();
        }
    }
}
