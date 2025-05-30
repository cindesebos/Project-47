using UnityEngine;
<<<<<<< Updated upstream
using UnityEngine.SceneManagement;
using System;
using Cysharp.Threading.Tasks;

namespace Scripts.Utils.Loader
{
    public class LevelLoader : ILevelLoader
    {
        public event Action OnLoadingStarted;

        public event Action OnLoadingFinished;

        public void LoadNextLevel() => LoadNextLevelAsync().Forget();

        public async UniTask LoadNextLevelAsync()
        {
            try
            {
                OnLoadingStarted?.Invoke();

                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                int nextSceneIndex = currentSceneIndex + 1;

                if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
                {
                    Debug.LogWarning("No next level to load. Already at the last scene.");

                    return;
                }

                Debug.Log($"Loading next level: Index {nextSceneIndex}");

                AsyncOperation loadOperation = SceneManager.LoadSceneAsync(nextSceneIndex, LoadSceneMode.Single);

                while (!loadOperation.isDone) await UniTask.Yield();

                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(nextSceneIndex));
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load next level: {ex.Message}");
            }
            finally
            {
                OnLoadingFinished?.Invoke();
            }
=======
using System;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Sources.Utils.Loader
{
    public class SceneLoader : ISceneLoader
    {
        public void LoadNextScene() => LoadNextSceneAsync().Forget();

        public async UniTask LoadNextSceneAsync()
        {
            int currentSceneId = SceneManager.GetActiveScene().buildIndex;
            int nextSceneId = currentSceneId + 1;

            if (nextSceneId >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.LogWarning("Next scene not found.");
                
                return;
            }

            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(nextSceneId, LoadSceneMode.Single);

            while (!loadSceneOperation.isDone)
                await UniTask.Yield();

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(nextSceneId));
>>>>>>> Stashed changes
        }
    }
}