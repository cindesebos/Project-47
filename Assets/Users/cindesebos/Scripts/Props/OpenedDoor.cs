using UnityEngine;

namespace Scripts.Props
{
    public class OpenedDoor : Door
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Character.Character>() || other.gameObject.GetComponent<Mutant.Mutant>()) Open();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Character.Character>() || other.gameObject.GetComponent<Mutant.Mutant>()) Close();
        }
    }
}