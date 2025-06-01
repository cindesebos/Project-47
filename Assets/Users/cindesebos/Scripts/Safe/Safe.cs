using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Scripts.Safe
{
    [RequireComponent(typeof(Outline))]
    public class Safe : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private UISafeLogic _uiLogic;
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

                _uiLogic.OnSafeOpened += OnOpened;
            }
        }

        private void Use(InputAction.CallbackContext context)
        {
            if (_isOpened) return;

            _uiLogic.OnOpen();
        }

        public void OnOpened()
        {
            _animator.enabled = true;

            _isOpened = true;
            _outline.OutlineWidth = 0;

            _characterInput.Interaction.Use.performed -= Use;

            RemoveExtraMaterials();
        }

        private void OnTriggerExit(Collider collider)
        {
            _outline.OutlineWidth = 0;

            if (collider.gameObject.GetComponent<Character.Character>())
            {
                _characterInput.Interaction.Use.performed -= Use;

                _uiLogic.OnSafeOpened -= OnOpened;
            }
        }

        private void RemoveExtraMaterials()
        {
            foreach (var renderer in GetComponents<Renderer>())
            {
                var originalMaterials = renderer.sharedMaterials;

                if (originalMaterials.Length > 1)
                {
                    var defaultMaterial = originalMaterials[0];

                    renderer.sharedMaterials = new[] { defaultMaterial };
                }
            }
        }
    }
}