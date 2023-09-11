using UnityEngine;

namespace SlimeCare
{
    public class SlimeCareStats : MonoBehaviour
    {
        public static SlimeCareStats Instance { get; private set; }

        // Sample stats for the slime.
        public int health = 100;
        public int happiness = 100;

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

            // Ensure the object survives when changing scenes.
            DontDestroyOnLoad(gameObject);
        }
    }
}
