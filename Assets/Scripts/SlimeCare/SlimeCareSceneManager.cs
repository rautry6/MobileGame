using System;
using System.Collections;
using System.Collections.Generic;
using JelloFelloInterfaces;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SlimeCare
{
    public class SlimeCareSceneManager : MonoBehaviour
    {
        public void LoadPlinkoScene()
        {
            StartCoroutine(OnPlinkoNavigation());
        }
        
        private IEnumerator OnPlinkoNavigation()
        {
            yield return StartCoroutine(SceneChangeInstance.Instance.FillImage());
            SceneManager.LoadScene("Plinko", LoadSceneMode.Single);
        }

        public void LoadSkeeballScene()
        {
            StartCoroutine(OnSkeeballNavigation());
        }

        private IEnumerator OnSkeeballNavigation()
        {
            yield return StartCoroutine(SceneChangeInstance.Instance.FillImage());
            SceneManager.LoadScene("Skeeball", LoadSceneMode.Single);
        }

        public void LoadSlimeHoleScene()
        {
            StartCoroutine(OnSlimeHoleNavigation());
        }

        private IEnumerator OnSlimeHoleNavigation()
        {
            yield return StartCoroutine(SceneChangeInstance.Instance.FillImage());
            SceneManager.LoadScene("New_SlimeHole", LoadSceneMode.Single);

        }

        public void LoadSlimeDeath()
        {
            StartCoroutine(OnSlimeDeathNavigation());
        }

        private IEnumerator OnSlimeDeathNavigation()
        {
            yield return StartCoroutine(SceneChangeInstance.Instance.FillImage());
            SceneManager.LoadScene("SlimeDeath", LoadSceneMode.Single);

        }
    }
}
