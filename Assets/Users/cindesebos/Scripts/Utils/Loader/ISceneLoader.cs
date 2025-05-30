using System;
using Cysharp.Threading.Tasks;

namespace Sources.Utils.Loader
{
    public interface ISceneLoader
    {
        void LoadNextScene();

        UniTask LoadNextSceneAsync();
    }
}