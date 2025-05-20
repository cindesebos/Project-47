using System;
using Scripts.Character;
using UnityEngine;
using Zenject;
using Scripts.Character.Inventory;

namespace Scripts.Levels
{
    public class LevelInstaller : MonoInstaller
    {
        private const string CharacterDataPath = "Datas/Character/Character Data";

        public override void InstallBindings()
        {
            BindCharacterInput();
            BindCharacterData();
            BindInventory();
        }

        private void BindCharacterInput()
        {
            Container.BindInterfacesAndSelfTo<CharacterInput>()
                .AsSingle();
        }

        private void BindCharacterData()
        {
            CharacterData characterData = Resources.Load<CharacterData>(CharacterDataPath);

            if (characterData == null) throw new NullReferenceException($"CharacterData asset not found at path: {CharacterDataPath}");

            Container.Bind<CharacterData>()
                .FromInstance(characterData)
                .AsSingle();
        }

        private void BindInventory()
        {
            Container.Bind<IInventory>()
                .To<Inventory>()
                .AsSingle();
        }
    }
}
