using System;
using System.Collections.Generic;
using Script.Card.CardDeck;
using Script.Game;
using Script.Spawner;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Netcode.NetworkManager;

namespace Script.Networking
{
    public class TestNetcode : NetworkBehaviour
    {
       
        [SerializeField] private Button startHost;
        [SerializeField] private Button  startClient;
        [SerializeField] private Button  startGame;
        private NetworkList<int> hostDeck;
        private NetworkList<int> clientDeck;
        public GameObject CanvasGame;
        public Sprite HellBG, HeavenBG;
        public Image BGHell, BGHeaven;
        

        public List<int> HostInspectorList = new List<int>();
        public List<int> ClientInspectorList = new List<int>();
        private void Awake()
        {
            hostDeck = new NetworkList<int>();
            clientDeck = new NetworkList<int>();
            startHost.onClick.AddListener(() =>
            {
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
                StartGameClientRpc();
                FindObjectOfType<TurnBehaviour>().StartGame();
                Hide();
            });
        }

        private void Update()
        {
                HostInspectorList = NetworkListToList(hostDeck);
                ClientInspectorList = NetworkListToList(clientDeck);
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();               
            if (IsHost)
            {
                var list = new PlayerCardDeckInstance().PlayerDeck;
                foreach (var i in list) hostDeck.Add(i);
                list = new EnemyCardDeckInstance().EnemyDeck;
                foreach (var i in list) clientDeck.Add(i);
            }
        }


        [ClientRpc]
        private void StartGameClientRpc()
        {
            FindObjectOfType<PlayerSpawnerCards>().StartGame(IsHost ? HostInspectorList : ClientInspectorList);
            FindObjectOfType<EnemySpawnerCards>().StartGame(IsHost ? ClientInspectorList : HostInspectorList);
        }

        private List<int> NetworkListToList(NetworkList<int> networkList)
        {
            var list = new List<int>();
            foreach (var i in networkList) 
                list.Add(i);
            return list;
        }

        void Hide() => gameObject.GetComponent<NetworkObject>().Despawn();
    }
}
