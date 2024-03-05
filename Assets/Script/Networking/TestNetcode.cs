using System;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Netcode.NetworkManager;

namespace Script.Networking
{
    public class TestNetcode : MonoBehaviour
    {
       
        [SerializeField] private Button startHost;
        [SerializeField] private Button  startClient;

        private void Awake()
        {
            startHost.onClick.AddListener(() =>
            {
                Debug.Log("Start Host");
                Singleton.StartHost();
                Hide();
            });
            startClient.onClick.AddListener(() =>
            {
                Singleton.StartClient();
                Hide();
            });
        }


        void Hide()
        {
            gameObject.SetActive(false);    
        }
    }
}
