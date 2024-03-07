using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Lobby
{
    public class LobbyHolder : MonoBehaviour
    {
        public string name;
        public int playerCount;
        public Button thisLobbyButton;
        [SerializeField] private TextMeshProUGUI NameTxt;
        [SerializeField] private TextMeshProUGUI CountTxt;

        private void OnEnable()
        {
            StartCoroutine(OnEnableDelay());
        }

        private IEnumerator OnEnableDelay()
        {
            yield return new WaitForSeconds(0.1f);
            NameTxt.text = name;
            CountTxt.text = playerCount.ToString();
        }
    }
}
