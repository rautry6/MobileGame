using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkeeballScoreManager : MonoBehaviour
{
    [SerializeField] SkeeballInput input;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] ScoreSlider scoreSlider;
    public int score { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        this.score += score;
        scoreSlider.UpdateScore(this.score);

        input.ResetSlime();
    }
}
