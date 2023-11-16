using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeUpdateSkin : MonoBehaviour
{
    public SkinnedMeshRenderer skin;

    // Start is called before the first frame update
    void Start()
    {
        string slimeColor = PlayerPrefs.GetString("SlimeColor");
        List<float> cn = new List<float>();
        foreach (string index in slimeColor.Split(','))
        {
            float newFloat = float.Parse(index);
            cn.Add(newFloat);
        }
        skin.material.color = new Color(cn[0], cn[1], cn[2], cn[3]);
    }
}
