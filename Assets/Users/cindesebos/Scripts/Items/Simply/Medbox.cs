using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Scripts.Items.Simply
{
    [RequireComponent(typeof(Outline))]
    public class Medbox : MonoBehaviour
    {
        [SerializeField] private GameObject _medkitItem;
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private Animator _animator;
        [SerializeField] private Outline _outline;

        private bool _canOpen = true;

        [Inject] private CharacterInput _characterInput;

        private void OnValidate()
        {
            _outline ??= GetComponent<Outline>();
            if (_outline.OutlineWidth != 0) _outline.OutlineWidth = 0;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!_canOpen) return;

            _outline.OutlineWidth = 5;

            if (collider.gameObject.GetComponent<Character.Character>())
            {
                _characterInput.Interaction.Use.performed += Use;
            }
        }

        private void Use(InputAction.CallbackContext context)
        {
            if (!_canOpen) return;

            _canOpen = false;
            _animator.enabled = true;
            _collider.enabled = false;

            _medkitItem.SetActive(true);

            RemoveExtraMaterials();

            _outline.OutlineWidth = 0;
        }

        private void OnTriggerExit(Collider collider)
        {
            _outline.OutlineWidth = 0;

            if (_canOpen && collider.gameObject.GetComponent<Character.Character>())
            {
                _characterInput.Interaction.Use.performed -= Use;
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