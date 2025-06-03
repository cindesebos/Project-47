using System.Threading.Tasks;
using Scripts.Utils.Loader;
using UnityEngine;
using Zenject;

namespace Scripts.Levels
{
    public class NextLevelLoader : MonoBehaviour
    {
        private ILevelLoader _levelLoader;

        [Inject]
        private void Construct(ILevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
        }
        
        private async Task OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.GetComponent<Character.Character>()) await _levelLoader.LoadNextLevelAsync();
        }

    }
}
