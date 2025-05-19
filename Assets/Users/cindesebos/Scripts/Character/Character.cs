using UnityEngine;

namespace Scripts.Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterMovement _movement;

        private void OnValidate()
        {
            _movement ??= GetComponent<CharacterMovement>();
        }
    }
}
