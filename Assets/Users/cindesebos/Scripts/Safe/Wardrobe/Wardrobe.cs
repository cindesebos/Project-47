using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Scripts.Safe.Wardrobe
{
    [RequireComponent(typeof(Outline))]
    public class Wardrobe : MonoBehaviour
    {
        [SerializeField] private Collider _colliderToEnable;
        [SerializeField] private Animator _animator;
        [SerializeField] private UIWardrobeLogic _uiWardrobeLogic;
        [SerializeField] private Outline _outline;

        [Inject] private CharacterInput _characterInput;

        [SerializeField] private bool _isOpened = false;

        private void OnValidate()
        {
            _outline ??= GetComponent<Outline>();
            
            if (_outline.OutlineWidth != 0) _outline.OutlineWidth = 0;
        }

        private void Start() => _outline.OutlineWidth = 0;

        private void OnTriggerEnter(Collider collider)
        {
            _outline.OutlineWidth = 5;

            if (!_isOpened && collider.gameObject.GetComponent<Character.Character>())
            {
                _characterInput.Interaction.Use.performed += Use;

                _uiWardrobeLogic.OnWardrobeOpened += OnOpened;
            }
        }

        private void Use(InputAction.CallbackContext context)
        {
            if (_isOpened) return;

            _uiWardrobeLogic.OnOpen();
        }

        public void OnOpened()
        {
            _colliderToEnable.enabled = true;
            _animator.enabled = true;
            gameObject.SetActive(false);

            _isOpened = true;
            _outline.OutlineWidth = 0;

            _characterInput.Interaction.Use.performed -= Use;
        }

        private void OnTriggerExit(Collider collider)
        {
            _outline.OutlineWidth = 0;

            if (collider.gameObject.GetComponent<Character.Character>())
            {
                _characterInput.Interaction.Use.performed -= Use;

                _uiWardrobeLogic.OnWardrobeOpened += OnOpened;
            }
        }
        
    }
}