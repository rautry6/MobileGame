using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlimeCreationMaster : MonoBehaviour
{
    public TMP_InputField nameBox;
    public Image colorExample;
    public GameObject slime;

    Color[] colorSwaps = { Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.white, Color.yellow };
    MeshRenderer rend;
    int colorIndex = 0;

    private void Start()
    {
        rend = slime.GetComponent<MeshRenderer>();
        colorIndex = Random.Range(0, colorSwaps.Length);
        rend.material.color = colorSwaps[colorIndex];
        colorExample.color = colorSwaps[colorIndex];
    }

    public void NextColor ()
    {
        colorIndex++;
        if (colorIndex >= colorSwaps.Length)
        {
            colorIndex = 0;
        }
        rend.material.color = colorSwaps[colorIndex];
        colorExample.color = colorSwaps[colorIndex];
    }

    public void PrevColor ()
    {
        colorIndex--;
        if (colorIndex < 0)
        {
            colorIndex = colorSwaps.Length;
        }
        rend.material.color = colorSwaps[colorIndex];
        colorExample.color = colorSwaps[colorIndex];
    }

    public void SaveAndExit ()
    {
        if (nameBox.text != "")
        {
            PlayerPrefs.SetString("SlimeName", nameBox.text);
            PlayerPrefs.SetString("SlimeColor", rend.material.color.ToString());
            Debug.Log("Slime Color: " + PlayerPrefs.GetString("SlimeColor") + " - Name: " + PlayerPrefs.GetString("SlimeName"));
            SceneManager.LoadScene("SlimeCare");
            PlayerPrefs.SetInt("FirstPlay", 1);
        }
        else
        {
            nameBox.text = "Dont forget to put a name here!";
        }
    }
}
