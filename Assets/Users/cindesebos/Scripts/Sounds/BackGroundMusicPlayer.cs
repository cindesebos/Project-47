using UnityEngine;
using Zenject;

namespace Scripts.Sounds
{
    public class BackGroundMusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private SoundsContainer _soundsContainer;

        [Inject]
        private void Construct(SoundsContainer soundsContainer)
        {
            _soundsContainer = soundsContainer;
        }

        public void PlayMenuMusic()
        {
            _audioSource.clip = _soundsContainer.MenuMusic;
            _audioSource.Play();
        }

        public void PlayGameplayMusic()
        {
            _audioSource.clip = _soundsContainer.GameplayMusic;
            _audioSource.Play();
        }

    }
}
