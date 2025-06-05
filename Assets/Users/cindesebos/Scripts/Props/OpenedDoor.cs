using Scripts.Sounds;
using UnityEngine;
using Zenject;

namespace Scripts.Props
{
    public class OpenedDoor : Door
    {
        private bool _isOpened;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Character.Character>() || other.gameObject.GetComponent<Mutant.Mutant>())
            {
                if (_isOpened) return;

                _isOpened = true;

                Debug.Log("Door is opened");

                Open();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Character.Character>() || other.gameObject.GetComponent<Mutant.Mutant>())
            {
                if (!_isOpened) return;

                _isOpened = false;

                Debug.Log("Door is closed");

                Close();
            }
        }
    }
}