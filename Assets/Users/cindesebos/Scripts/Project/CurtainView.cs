using Scripts.Utils.Loader;
using UnityEngine;
using Zenject;

namespace Scripts.Project
{
    public class CurtainView : MonoBehaviour
    {
        [SerializeField] private GameObject _view;

        private ILevelLoader _levelLoader;

        [Inject]
        private void Construct(ILevelLoader levelLoader)
        {
            _levelLoader = levelLoader;

            _levelLoader.OnLoadingStarted += Show;
            _levelLoader.OnLoadingFinished += Hide;
        }

        private void Start() => Hide();

        public void Show() => _view.SetActive(true);

        public void Hide() => _view.SetActive(false);

        private void OnDestroy()
        {
            _levelLoader.OnLoadingStarted -= Show;
            _levelLoader.OnLoadingFinished -= Hide;
        }
    }
}