using System;
using Scripts.Character;
using UnityEngine;
using Zenject;
using Scripts.Character.Inventory;
using Scripts.Items;

namespace Scripts.Levels
{
    public class LevelInstaller : MonoInstaller
    {
        private const string CharacterDataPath = "Datas/Character/Character Data";
        private const string InventorySlotsHandlerDataPath = "Datas/Inventory Slots Handler Data";

        public override void InstallBindings()
        {
            BindCharacterInput();
            BindCharacterData();
            BindCharacter();
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

        private void BindCharacter()
        {
            Character.Character character = FindObjectOfType<Character.Character>();

            Container.Bind<Character.Character>()
                .FromInstance(character)
                .AsSingle();
        }

        private void BindInventory()
        {
            InventorySlotsHandlerData inventorySlotsHandlerData = Resources.Load<InventorySlotsHandlerData>(InventorySlotsHandlerDataPath);

            if (inventorySlotsHandlerData == null) throw new NullReferenceException($"InventorySlotsHandlerData asset not found at path: {InventorySlotsHandlerDataPath}");

            Container.Bind<InventorySlotsHandlerData>()
                .FromInstance(inventorySlotsHandlerData)
                .AsSingle();

            Container.Bind<IInventory>()
                .To<Inventory>()
                .AsSingle();

            Container.Bind<InventorySlotsHandler>()
                .AsSingle()
                .NonLazy();

            Container.Bind<ItemsCaller>()
                .AsSingle()
                .NonLazy();
        }
    }
}