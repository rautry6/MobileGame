using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SlimeCare
{
    public class SlimeCareStats : MonoBehaviour
    {
        public static SlimeCareStats Instance { get; private set; }
        // Sample stats for the slime.
        public float health;
        public float happiness;
        
        private float maxHealth = 600f;
        private float maxHappiness = 600f;
        private Slider _healthSlider;
        private Slider _happinessSlider;
        
        private void Awake()
        {
            // Check if an instance already exists
            if (Instance != null && Instance != this)
            {
                // If an instance already exists and it's not this, then destroy this. This enforces our singleton pattern.
                Destroy(gameObject);
                return;
            }

            // If no instance exists, then this becomes the instance.
            Instance = this;
            health = maxHealth;
            happiness = maxHappiness;
            // Ensure the object survives when changing scenes.
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            FindAndAssignSliders();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "SlimeCare")
            {
                FindAndAssignSliders();
            }
        }

        private void FindAndAssignSliders()
        {
            _healthSlider = GameObject.FindWithTag("HealthSlider").GetComponent<Slider>();
            _healthSlider.value = health;
            _happinessSlider = GameObject.FindWithTag("HappinessSlider").GetComponent<Slider>();
            _happinessSlider.value = happiness;
        }

        private void Update()
        {
            health -= Time.deltaTime;
            happiness -= Time.deltaTime;

            if (_healthSlider != null && _happinessSlider != null)
            {
                _healthSlider.value = health;
                _happinessSlider.value = happiness;
            }
        }

        /// <summary>
        /// Pass values to this function that will be added to the current health and happiness values
        /// negative values decrease positive values increase
        /// </summary>
        /// <param name="healthValue"></param>
        /// <param name="happinessValue"></param>
        public void UpdateHealthAndHappiness(float healthValue, float happinessValue)
        {
            health += healthValue;
            happiness += happinessValue;
            
            if (health > maxHappiness)
            {
                health = maxHealth;
            }

            if (happiness > maxHappiness)
            {
                happiness = maxHappiness;
            }
        }
    }
}
