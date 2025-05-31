using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Mutant
{
    public class MutantChaser : MonoBehaviour
    {
        [SerializeField] private Transform _eyePosition;
        [SerializeField] private float _visionRange = 20f;

        private void Update()
        {
            if (Physics.Raycast(_eyePosition.position, _eyePosition.forward, out RaycastHit hit, _visionRange))
            {
                if (hit.collider.GetComponent<Character.Character>())
                {
                    Debug.Log("тест");
                }
            }

            Debug.DrawRay(_eyePosition.position, _eyePosition.forward * _visionRange, Color.red);
        }
    }
}
