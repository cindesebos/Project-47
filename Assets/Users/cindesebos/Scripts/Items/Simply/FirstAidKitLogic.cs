using Scripts.UI;
using UnityEngine;

namespace Scripts.Items.Simply
{
    public class FirstAidKitLogic
    {
        public int Amount
        {
            get => _amount;
            private set
            {
                _amount = value;

                Debug.Log($"Setting med kits amount to: {_amount}");

                _hudView.SetMedKitsAmount(_amount);
            }
        }

        private int _amount;
        private HudView _hudView;

        public FirstAidKitLogic(HudView hudView)
        {
            _hudView = hudView;

            _hudView.SetMedKitsPanelActive(true);

            _hudView.SetMedKitsAmount(Amount);
        }

        public void Add(int amount)
        {
            Debug.Log($"Adding {amount} med kits. Current amount: {Amount}");

            Amount += amount;
        }

        public void Remove(int amount)
        {
            Debug.Log($"Removing {amount} med kits. Current amount: {Amount}");

            if (Amount <= 0) return;

            Amount -= amount;
        }
    }
}