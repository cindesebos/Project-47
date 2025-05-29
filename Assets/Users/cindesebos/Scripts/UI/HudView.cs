using UnityEngine;
using TMPro;

namespace Scripts.UI
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private GameObject _hearts;
        [Space]

        [SerializeField] private GameObject _gunPanel;
        [SerializeField] private TextMeshProUGUI _ammoAmountDisplay;
        [Space]

        [SerializeField] private GameObject _medKitsPanel;
        [SerializeField] private TextMeshProUGUI _medKitsAmountDisplay;

        public void SetGunPanelActive(bool isActive) => _gunPanel.SetActive(isActive);

        public void SetAmmoAmount(int amount)
        {
            Debug.Log($"Setting ammo amount to: {amount}");

            _ammoAmountDisplay.text = amount.ToString();
        }

        public void SetMedKitsPanelActive(bool isActive) => _medKitsPanel.SetActive(isActive);

        public void SetMedKitsAmount(int amount) => _medKitsAmountDisplay.text = amount.ToString()+"x";
    }
}