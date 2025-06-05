using System.Collections;
using Scripts.Items.Simply;
using Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Scripts.Character
{
    public class CharacterHealth : MonoBehaviour
    {
        private const string HealthKey = "Save_Health";

        [field: SerializeField] public float Health { get; private set; }
        [SerializeField] private bool _isFirstLevel;
        private float delay = 3;

        private FirstAidKitLogic _firstAidKitLogic;
        private HudView _hudView;
        private bool _isDead;
        
        [Inject]
        private void Construct(FirstAidKitLogic firstAidKitLogic, HudView hudView)
        {
            _firstAidKitLogic = firstAidKitLogic;
            _hudView = hudView;
        }

        private void Start()
        {
            if(!_isFirstLevel) Load();

            _hudView.SetCharacterHealth(this);

            _hudView.SetHealth(Health);
        }

        public void ApplyDamage(float damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                _isDead = true;

                StartCoroutine(DeadDelay());
            }

            _hudView.SetHealth(Health);
        }

        private IEnumerator DeadDelay()
        {
            _hudView._deadDisplayer.SetActive(true);

            yield return new WaitForSeconds(delay);
            
            _hudView._deadDisplayer.SetActive(false);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnSetHealth(float health)
        {
            Debug.Log("Heal character: " + health);

            Health += health;

            _hudView.SetHealth(Health);
        }

        private void OnDestroy()
        {
            if (_isDead) return;

            Save();
        }

        public void Save() => PlayerPrefs.SetFloat(HealthKey, Health);

        public void Load()
        {
            float tempHealth = PlayerPrefs.GetFloat(HealthKey, Health);

            Health = tempHealth;

            _hudView.SetHealth(Health);
        }
    }
}