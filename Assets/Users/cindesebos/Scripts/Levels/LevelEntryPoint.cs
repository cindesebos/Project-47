using UnityEngine;
using Scripts.Utils.Storage;
using Zenject;

namespace Scripts.Levels
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [SerializeField] private bool _needToResetStorage = false;

        private IStorageService _storageService;

        [Inject]
        private void Construct(IStorageService storageService)
        {
            _storageService = storageService;
        }

        private void Start()
        {
            if (_needToResetStorage) _storageService.Reset();

            _storageService.Load();
        }

        private void OnDisable()
        {
            _storageService.Save();
        }
    }
}
