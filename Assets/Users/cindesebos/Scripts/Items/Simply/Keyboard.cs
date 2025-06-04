using Scripts.Props;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Scripts.Items.Simply
{
    [RequireComponent(typeof(Outline))]
    public class Keyboard : MonoBehaviour
    {
        [SerializeField] private GameObject _greenBlock, _redBlock;
        [SerializeField] private ClosedDoor _door;
        [SerializeField] private Outline _outline;

        [SerializeField] private bool _canUse = true;
        [SerializeField] private bool _isUsed = false;

        [Inject] private CharacterInput _characterInput;

        private void OnValidate()
        {
            _outline ??= GetComponent<Outline>();
            if (_outline.OutlineWidth != 0) _outline.OutlineWidth = 0;
        }

        private void Start() => _outline.OutlineWidth = 0;

        private void OnTriggerEnter(Collider collider)
        {
            if (_isUsed) return;

            _outline.OutlineWidth = 5;

            if (collider.gameObject.GetComponent<Character.Character>())
            {
                _characterInput.Interaction.Use.performed += Use;
            }
        }

        private void Use(InputAction.CallbackContext context)
        {
            if (_isUsed) return;

            _isUsed = true;

            _outline.OutlineWidth = 0;
            _redBlock.SetActive(false);
            _greenBlock.SetActive(true);

            _door.Open();
        }

        private void OnTriggerExit(Collider collider)
        {
            if (_isUsed) return;

            _outline.OutlineWidth = 0;

            if (_canUse && collider.gameObject.GetComponent<Character.Character>())
            {
                _characterInput.Interaction.Use.performed -= Use;
            }
        }
    }
}