<<<<<<< Updated upstream
using Scripts.Utils.Loader;
=======
using Sources.Utils.Loader;
>>>>>>> Stashed changes
using UnityEngine;
using Zenject;

namespace Scripts.Levels
{
    public class NextLevelLoader : MonoBehaviour
    {
<<<<<<< Updated upstream
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

=======
        private ISceneLoader _sceneLoader;

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Character.Character>())
            {
                _sceneLoader.LoadNextScene();
            }
        }
>>>>>>> Stashed changes
    }
}
