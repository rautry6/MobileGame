using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeeballHole : MonoBehaviour
{
    [SerializeField] Scores score;
    [SerializeField] SkeeballScoreManager scoreManager;

    ScoresFunctions scoresFunctions;
    // Start is called before the first frame update
    void Start()
    {
        scoresFunctions = new ScoresFunctions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slime"))
        {
            scoreManager.AddScore(scoresFunctions.ConvertToInt(score));
        }
    }
}
