using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Menu.Buttons
{
    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField] protected Button Button;

        private void OnValidate() => Button ??= GetComponent<Button>();
    }
}
