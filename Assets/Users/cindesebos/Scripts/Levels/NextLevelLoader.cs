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
        
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.GetComponent<Character.Character>()) _levelLoader.LoadNextLevelAsync();
        }

    }
}
