using UnityEngine;
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
        }
    }
}