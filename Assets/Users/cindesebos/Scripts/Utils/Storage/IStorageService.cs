using UnityEngine;

namespace Scripts.Utils.Storage
{
    public interface IStorageService
    {
        void Save();
        void Load();
        void Reset();
    }
}