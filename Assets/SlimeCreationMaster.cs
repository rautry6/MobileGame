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
    SkinnedMeshRenderer rend;
    int colorIndex = 0;

    private void Start()
    {
        rend = slime.GetComponent<SkinnedMeshRenderer>();
        colorIndex = Random.Range(0, colorSwaps.Length);
        rend.material.color = colorSwaps[colorIndex];
        //rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 213);
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
        //rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 213);
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
        //rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 213);
        colorExample.color = colorSwaps[colorIndex];
    }

    public void SaveAndExit ()
    {
        if (nameBox.text != "")
        {
            PlayerPrefs.SetString("SlimeName", nameBox.text);
            string slimeColorString = rend.material.color.r + "," +
                rend.material.color.g + "," +
                rend.material.color.b + "," +
                rend.material.color.a;
            PlayerPrefs.SetString("SlimeColor", slimeColorString);
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
