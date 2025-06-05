using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneReloader : MonoBehaviour
{
    private PlayerInput _playerInput;

    [SerializeField] private InputActionReference reloadActionReference;

    private void Awake()
    {
        // Если не назначен в инспекторе — создаём стандартное действие
        if (reloadActionReference == null)
        {
            var asset = new InputActionAsset();
            var map = asset.AddActionMap("UI");
            var action = map.AddAction("Reload", binding: "<Keyboard>/r");

            reloadActionReference = ScriptableObject.CreateInstance<InputActionReference>();
            reloadActionReference.Asset = action;

            reloadActionReference.Asset.Enable();
        }
        else
        {
            reloadActionReference.action.Enable();
        }

        reloadActionReference.action.performed += OnReloadPerformed;
    }

    private void OnReloadPerformed(InputAction.CallbackContext context)
    {
        ReloadScene();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        if (reloadActionReference != null)
        {
            reloadActionReference.action.performed -= OnReloadPerformed;
            reloadActionReference.action.Disable();
        }
    }
}