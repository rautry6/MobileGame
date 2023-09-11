using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeCareSceneManager : MonoBehaviour
{
    public void LoadPlinkoScene()
    {
        SceneManager.LoadScene("Plinko", LoadSceneMode.Single);
    }
    public void LoadSkeeballScene()
    {
        SceneManager.LoadScene("Skeeball", LoadSceneMode.Single);
    }
    public void LoadSlimeHoleScene()
    {
        SceneManager.LoadScene("SlimeHole", LoadSceneMode.Single);
    }
    
}
