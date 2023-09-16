using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class SceneChangeInstance : MonoBehaviour
    {
        public static SceneChangeInstance Instance { get; private set; }

        [SerializeField] private Image sceneChangeImage;
        [SerializeField] private float fillSpeed;
        
        private List<Image.FillMethod> _fillMethods;
        private bool _hasFilled;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _fillMethods = new()
            {
                Image.FillMethod.Horizontal,
                Image.FillMethod.Vertical,
                Image.FillMethod.Radial90,
                Image.FillMethod.Radial180,
                Image.FillMethod.Radial360
            };
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            Debug.Log($"SceneName: {scene.name}");
            StartCoroutine(ReduceImageFillToZero());
        }

        public IEnumerator FillImage()
        {
            _hasFilled = false;
            var fillType = GetRandomFillType();
            sceneChangeImage.fillMethod = fillType;
            switch (fillType)
            {
                case Image.FillMethod.Horizontal:
                {
                    sceneChangeImage.fillOrigin = Random.Range(0, 2);
                    break;
                }
                case Image.FillMethod.Vertical:
                {
                    sceneChangeImage.fillOrigin = Random.Range(0, 2);
                    break;
                }
                default:
                {
                    if (fillType is Image.FillMethod.Radial90 or Image.FillMethod.Radial180 or Image.FillMethod.Radial360)
                    {
                        sceneChangeImage.fillOrigin = Random.Range(0, 4);    
                    }
                    break;
                }
            }

            StartCoroutine(FillImageToOne());
            yield return new WaitUntil(() => _hasFilled);
        }

        private IEnumerator FillImageToOne()
        {
            while (sceneChangeImage.fillAmount < .998f)
            {
                sceneChangeImage.fillAmount += fillSpeed * Time.deltaTime;
                yield return null;
            }

            sceneChangeImage.fillAmount = 1.0f;
            _hasFilled = true;
        }

        private Image.FillMethod GetRandomFillType()
        {
            var randomFillIndex = Random.Range(0, _fillMethods.Count);
            return _fillMethods[randomFillIndex];
        }

        private IEnumerator ReduceImageFillToZero()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            while (sceneChangeImage.fillAmount > 0.002f)
            {
                sceneChangeImage.fillAmount -= fillSpeed * Time.deltaTime;
                yield return null;
            }

            sceneChangeImage.fillAmount = 0;
        }
    }
}