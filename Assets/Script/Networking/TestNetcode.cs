using System;
using Script.Spawner;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Netcode.NetworkManager;

namespace Script.Networking
{
    public class TestNetcode : MonoBehaviour
    {
       
        [SerializeField] private Button startHost;
        [SerializeField] private Button  startClient;
        [SerializeField] private Button  startGame;
        public GameObject CanvasGame;
        public Sprite HellBG, HeavenBG;
        public Image BGHell, BGHeaven;
        

        private void Awake()
        {
            startHost.onClick.AddListener(() =>
            {
                Debug.Log("Start Host");
                Singleton.StartHost();
                startGame.gameObject.SetActive(true);
                BGHeaven.sprite = HeavenBG;
            });
            startClient.onClick.AddListener(() =>
            {
                Singleton.StartClient();
                BGHell.sprite = HellBG;
            });
            startGame.gameObject.SetActive(false);
            startGame.onClick.AddListener(() =>
            {
                CanvasGame.SetActive(true);
                FindObjectOfType<PlayerSpawnerCards>().StartGame();
                FindObjectOfType<EnemySpawnerCards>().StartGame();
                Hide();
            });
        }


        [ClientRpc]
        void Hide()
        {
            gameObject.GetComponent<NetworkObject>().Despawn();
        }
    }
}
