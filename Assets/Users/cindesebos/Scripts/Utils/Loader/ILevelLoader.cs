using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

namespace Scripts.Utils.Loader
{
    public interface ILevelLoader
    {
        event Action OnLoadingStarted;
        
        event Action OnLoadingFinished;

        void LoadNextLevel();

        UniTask LoadNextLevelAsync();
    }
}
