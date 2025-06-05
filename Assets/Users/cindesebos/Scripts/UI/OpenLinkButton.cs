using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class OpenLinkButton : MonoBehaviour
    {
        [SerializeField] private string _title;
        [SerializeField] private string _link;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _button;

        private void OnValidate()
        {
            _button ??= GetComponent<Button>();
        }

        private void Start()
        {
            Initialize();

            _button.onClick.AddListener(() => OnClick());
        }

        private void Initialize() => _text.text = _title;

        private void OnClick() => Application.OpenURL(_link);
    }
}
