using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using System.Collections;
using System.Collections.Generic;

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
        private Material _slimeMaterial;
        private float _redComponent, _greenComponent, _blueComponent;
        private string _slimeName;
        private bool _shouldTick;
        private Material _transparentMaterial;

        private Prize wonPrize;

        public Prize WonPrize => wonPrize;

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
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "SlimeCare")
            {
                _shouldTick = true;
                FindAndAssignSliders();
                _slimeName = PlayerPrefs.GetString("SlimeName");
                var slimeColor = PlayerPrefs.GetString("SlimeColor");
                var formattedColor = slimeColor.Substring(5, slimeColor.Length - 6);
                var rgbaComponents = formattedColor.Split(',');
                _redComponent = (float) double.Parse(rgbaComponents[0]);
                _greenComponent = (float) double.Parse(rgbaComponents[1]);
                _blueComponent = (float) double.Parse(rgbaComponents[2]);
                _slimeMaterial = (Material)Resources.Load("Slime", typeof(Material));
                _transparentMaterial = (Material) Resources.Load("Tranparent", typeof(Material));
                _slimeMaterial.color = new Color(_redComponent, _greenComponent, _blueComponent, 1f);
                _transparentMaterial.SetColor("_Color", new Color(_redComponent, _greenComponent, _blueComponent, 0.5f));

                Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

                Debug.Log(inventory);

                inventory.AddPrizeToInventory(wonPrize);
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
            if (!_shouldTick) return;
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

        public void SetPrize(Prize prize)
        {
            wonPrize = prize;
        }

       
    }
}
