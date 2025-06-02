using UnityEngine;
using Zenject;
using Scripts.UI;
using Scripts.Utils.Loader;
using System;
using Scripts.Items.Simply;
using Scripts.Sounds;

namespace Scripts.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private CursorHandler _cursorHandler;
        [SerializeField] private HudView _hudView;
        [SerializeField] private SoundsContainer _soundsContainer;
        [SerializeField] private BackGroundMusicPlayer _backGroundMusicPlayer;

        public override void InstallBindings()
        {
            BindSoundsContainer();
            BindFirstAidKitLogic();
            BindUIInput();
            BindLevelLoader();
            BindCursorHandler();
            BindHudView();
            BindBackGroundMusicPlayer();
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
    }
}
