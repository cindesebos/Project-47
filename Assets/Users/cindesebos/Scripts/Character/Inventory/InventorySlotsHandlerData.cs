using UnityEngine;

namespace Scripts.Character.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Slots Handler Data", menuName = "Datas/New Inventory Slots Handler Data")]
    public class InventorySlotsHandlerData : ScriptableObject
    {
        [field: SerializeField] public string GunItemName { get; private set; } = "Пистолет";
        [field: SerializeField] public string AmmoItemName { get; private set; } = "Патроны";
        [field: SerializeField] public int AmmoAmount { get; private set; } = 20;
        [field: SerializeField] public string FirstAidKitItemName { get; private set; } = "Аптечка";
        [field: SerializeField] public int FirstAidKitAmount { get; private set; } = 1;
    }
}