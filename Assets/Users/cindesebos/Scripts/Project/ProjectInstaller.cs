using UnityEngine;
using Zenject;
using Scripts.UI;
using Scripts.Utils.Loader;
using System;
using Scripts.Items.Simply;

namespace Scripts.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ArtsToggler _artsToggler;
        [SerializeField] private CursorHandler _cursorHandler;
        [SerializeField] private HudView _hudView;

        public override void InstallBindings()
        {
            BindFirstAidKitLogic();
            BindUIInput();
            BindArtsHandler();
            BindLevelLoader();
            BindCursorHandler();
            BindHudView();
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

        private void BindArtsHandler()
        {
            Container.Bind<ArtsToggler>()
                .FromInstance(_artsToggler)
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
    }
}
