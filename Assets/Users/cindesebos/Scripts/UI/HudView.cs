using UnityEngine;
using TMPro;

namespace Scripts.UI
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private GameObject[] _hearts;
        [Space]

        [SerializeField] private GameObject _gunPanel;
        [SerializeField] private TextMeshProUGUI _ammoAmountDisplay;
        [Space]

        [SerializeField] private GameObject _medKitsPanel;
        [SerializeField] private TextMeshProUGUI _medKitsAmountDisplay;
        [SerializeField] public GameObject _deadDisplayer;

        private Character.CharacterHealth _characterHealth;

        public void SetCharacterHealth(Character.CharacterHealth character) => _characterHealth = character;

        public void SetGunPanelActive(bool isActive) => _gunPanel.SetActive(isActive);

        public void SetAmmoAmount(float amount)
        {
            Debug.Log($"Setting ammo amount to: {amount}");

            _ammoAmountDisplay.text = amount.ToString();
        }

        public void SetHealth(float health)
        {
            int clampedHealth = Mathf.Clamp((int)health, 0, _hearts.Length);

            for (int i = 0; i < _hearts.Length; i++) _hearts[i].SetActive(i < clampedHealth);
        }

        public void SetMedKitsPanelActive(bool isActive) => _medKitsPanel.SetActive(isActive);

        public void HealCharacter(float amount) => _characterHealth.OnSetHealth(amount);

        public void SetMedKitsAmount(float amount) => _medKitsAmountDisplay.text = amount.ToString() + "x";
    }
}