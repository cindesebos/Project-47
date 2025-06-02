using Scripts.Utils.Loader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Menu.Buttons
{
    public class PlayNextLevelButton : BaseButton
    {
        [Inject]
        private async void Construct(ILevelLoader levelLoader)
        {
            this.Button.onClick.AddListener(async delegate
            {
                await levelLoader.LoadNextLevelAsync();
            });
        }
    
    }
}
