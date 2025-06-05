using UnityEngine;
using Scripts.Utils.Storage;
using Zenject;
using Scripts.UI;
using Scripts.Sounds;
using Scripts.Character.Inventory;
using Scripts.Items;
using Scripts.Menu;
using Scripts.Items.Simply;

namespace Scripts.Levels
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [SerializeField] private bool _needToResetStorage = false;
        [SerializeField] private bool _needToStartGameplayMusic = false;
        [SerializeField] private ItemData _axeItemData;

        private IStorageService _storageService;
        private CursorHandler _cursorHandler;
        private BackGroundMusicPlayer _backGroundMusicPlayer;
        private IInventory _inventory;
        private InventoryNotification _inventoryNotification;
        private SettingsHandler _settingsHandler;
        private FirstAidKitLogic _firstAidKitLogic;
        private Character.Character _character;

        [Inject]
        private void Construct(IStorageService storageService, CursorHandler cursorHandler, BackGroundMusicPlayer backGroundMusicPlayer,
        IInventory inventory, InventoryNotification inventoryNotification, SettingsHandler settingsHandler,
        FirstAidKitLogic firstAidKitLogic, Character.Character character)
        {
            _storageService = storageService;
            _cursorHandler = cursorHandler;
            _backGroundMusicPlayer = backGroundMusicPlayer;
            _inventory = inventory;
            _inventoryNotification = inventoryNotification;
            _settingsHandler = settingsHandler;
            _firstAidKitLogic = firstAidKitLogic;
        }

        private void Start()
        {
            _storageService.Initialize(_firstAidKitLogic, _character, _settingsHandler);

            if (_needToResetStorage) _storageService.Reset();

            _inventoryNotification.Initialize(_inventory);

            _storageService.Load();
            _cursorHandler.SetVisibility(false);
            
            if (_needToStartGameplayMusic) _backGroundMusicPlayer.PlayGameplayMusic();

            if (_axeItemData != null) _inventory.TryAddItem(_axeItemData);
        }

        private void OnDisable()
        {
            _storageService.Save();
        }
    }
}
