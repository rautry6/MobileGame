using System;
using TMPro;
using UnityEngine;

namespace SlimeCare
{
    public class SlimeNameManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text slimeNameText;

        private void Start()
        {
            slimeNameText.SetText(PlayerPrefs.GetString("SlimeName"));
        }
    }
}
