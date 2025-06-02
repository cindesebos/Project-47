using Scripts.UI;
using UnityEngine;
using Zenject;

namespace Scripts.Menu
{
    public class MenuEntryPoint : MonoBehaviour
    {
        private CursorHandler _cursorHandler;

        [Inject]
        private void Construct(CursorHandler cursorHandler)
        {
            _cursorHandler = cursorHandler;
        }

        private void Start()
        {
            _cursorHandler.SetVisibility(true);
        }
    }
}
