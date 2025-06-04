using Scripts.Character.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Scripts.Items.Simply
{
    [RequireComponent(typeof(Outline))]
    public class Exoskeleton : MonoBehaviour
    {
        [SerializeField] private Outline _outline;

        private CharacterInput _characterInput;
        private Character.Character _character;

        [Inject]
        private void Construct(CharacterInput characterInput, Character.Character character)
        {
            _characterInput = characterInput;
            _character = character;
        }

        private void OnValidate()
        {
            _outline ??= GetComponent<Outline>();
            if (_outline.OutlineWidth != 0) _outline.OutlineWidth = 0;
        }

        private void Start() => _outline.OutlineWidth = 0;

        private void OnTriggerStay(Collider collider)
        {
            _outline.OutlineWidth = 5;

            if (collider.gameObject.GetComponent<Character.Character>())
            {
                _characterInput.Interaction.Use.performed += Use;
            }
        }

        private bool _isUsed;

        private void Use(InputAction.CallbackContext context)
        {
            if (_isUsed) return;

            _isUsed = true;

            _outline.OutlineWidth = 0;

            _character.AllowRun();

            Destroy(gameObject);
        }

        private void OnTriggerExit(Collider collider)
        {
            _outline.OutlineWidth = 0;

            if (collider.gameObject.GetComponent<Character.Character>())
            {
                _characterInput.Interaction.Use.performed -= Use;
            }
        }
    }
}