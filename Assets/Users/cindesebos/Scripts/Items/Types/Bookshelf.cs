using Scripts.Character.Inventory;
using Scripts.Sounds;
using UnityEngine;
using Zenject;
using Scripts.Utils;

namespace Scripts.Items.Types
{
    public class Bookshelf : InteractableItem
    {
        [SerializeField] private BoxCollider[] _collidersToEnable;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Animator _animator;

        private SoundsContainer _soundsContainer;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
            _audioSource ??= GetComponent<AudioSource>();
        }

        [Inject]
        private void Construct(SoundsContainer soundsContainer)
        {
            _soundsContainer = soundsContainer;
        }

        private void Start() => _animator.enabled = false;

        protected override void OnItemPickedUp(ItemData item)
        {
            if (_targetItemId != item.Id) return;

            _audioSource.PlayOneShot(_soundsContainer.MovingBookshelfSound);

            _animator.enabled = true;

            foreach(var collider in _collidersToEnable) collider.enabled = true;
        }
    }
}