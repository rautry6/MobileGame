using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSlider : MonoBehaviour
{
    [SerializeField] SlimeThrowUI slimeThrow;
    [SerializeField] TMP_Text scoreText;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        slider.maxValue = slimeThrow.numberOfThrows * 10000;
    }

    public void UpdateScore(int score)
    {
        slider.value = score;
        scoreText.text = score.ToString();
    }
}
