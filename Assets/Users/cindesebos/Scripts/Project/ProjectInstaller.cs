using UnityEngine;
using Zenject;
using Scripts.UI;
using Scripts.Utils.Loader;
using System;
using Scripts.Items.Simply;
using Scripts.Sounds;
using Scripts.Character.Inventory;
using TMPro;
using Scripts.UI.FPS;

namespace Scripts.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private CursorHandler _cursorHandler;
        [SerializeField] private HudView _hudView;
        [SerializeField] private SoundsContainer _soundsContainer;
        [SerializeField] private BackGroundMusicPlayer _backGroundMusicPlayer;
        [SerializeField] private InventoryNotification _inventoryNotification;
        [SerializeField] private FPSDisplayer _fpsDisplayer;

        public override void InstallBindings()
        {
            BindSoundsContainer();
            BindFirstAidKitLogic();
            BindUIInput();
            BindLevelLoader();
            BindCursorHandler();
            BindHudView();
            BindBackGroundMusicPlayer();
            BindInventoryNotification();
            BindFpsDisplayer();
        }

        private void BindFirstAidKitLogic()
        {
            Container.Bind<FirstAidKitLogic>()
                .AsSingle()
                .NonLazy();
        }

        private void BindUIInput()
        {
            Container.BindInterfacesAndSelfTo<UIInput>()
                .AsSingle();
        }

        private void BindLevelLoader()
        {
            Container.Bind<ILevelLoader>()
                .To<LevelLoader>()
                .AsSingle();
        }

        private void BindCursorHandler()
        {
            Container.Bind<CursorHandler>()
                .FromInstance(_cursorHandler)
                .AsSingle();
        }

        private void BindHudView()
        {
            Container.Bind<HudView>()
                .FromInstance(_hudView)
                .AsSingle();
        }

        private void BindSoundsContainer()
        {
            Container.Bind<SoundsContainer>()
                .FromInstance(_soundsContainer)
                .AsSingle();
        }

        private void BindBackGroundMusicPlayer()
        {
            Container.Bind<BackGroundMusicPlayer>()
                .FromInstance(_backGroundMusicPlayer)
                .AsSingle();
        }

        private void BindInventoryNotification()
        {
            Container.Bind<InventoryNotification>()
                .FromInstance(_inventoryNotification)
                .AsSingle();
        }

        private void BindFpsDisplayer()
        {
            Container.Bind<FPSDisplayer>()
                .FromInstance(_fpsDisplayer)
                .AsSingle();
        }
    }
}
