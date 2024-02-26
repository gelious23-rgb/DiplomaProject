using Script.Spawner;
using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Characters.Player
{
    public class Player: NetworkBehaviour
    {
        

        [SerializeField] private NetworkObject _player;

        void Start()
        {
            SetUpPlayer();
        }

        private void SetUpPlayer()
        {

            Vector3 spawnPosition;
            
            if (NetworkManager.Singleton.ConnectedClientsList.Count > 1)
            {
                spawnPosition = new Vector3(0, 800, 0);
            }
            else
            {
                spawnPosition = new Vector3(0, 0, 0);
            }

            NetworkObject player = Instantiate(_player, spawnPosition, Quaternion.identity, transform);

            if (NetworkManager.Singleton.ConnectedClientsList.Count > 1)
            {

             player.transform.rotation = Quaternion.Euler(0, 180, 0);

            }

        }

    
    }
    
    

}
