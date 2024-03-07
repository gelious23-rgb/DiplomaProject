using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Script.Lobby
{
    public class LobbyNetcode : MonoBehaviour
    {
       [SerializeField] private string lobbyName = "DefaultLobbyName";
        
        [SerializeField] private GameObject ListOfLobbiesObj;
        [SerializeField] private Transform Container;
        private Unity.Services.Lobbies.Models.Lobby hostLobby;
        private float HeartBeattimer;
       
        private async void OnEnable()
        {
            await UnityServices.InitializeAsync();
             AuthenticationService.Instance.SignedIn += () =>
            {
                Debug.Log("Signed in "+ AuthenticationService.Instance.PlayerId);
            };
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        private void Update()
        {
            HandleHeartBeat();
        }

        public void RefreshList()
        {
             
        }

        private async void HandleHeartBeat()
        {
            if (hostLobby != null)
            {
                HeartBeattimer -= Time.deltaTime;
                if (HeartBeattimer < 0f)
                {
                    float timerMax = 15f;
                    HeartBeattimer = timerMax;
                   await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
                   RefreshList();
                }
            }
        }

        [ContextMenu("Create Lobby")]
        private async void CreateLobby()
        {
            try
            {
                var thisLobby = await  LobbyService.Instance.CreateLobbyAsync(lobbyName, 2);
                hostLobby = thisLobby;
                Debug.Log("Created Lobby "+lobbyName);
                
            }
            catch (LobbyServiceException e)
            {
               Debug.Log(e);
            }
        }
        [ContextMenu("List Lobbies")]
        private async void ListLobbies()
        {
            try
            {
                var res = await Lobbies.Instance.QueryLobbiesAsync();
                Debug.Log("Found " + res.Results.Count + " lobbies");
                foreach (var lobby in res.Results)
                {
                    var lobbyobj = Instantiate(ListOfLobbiesObj, Container);
                        var holder = lobbyobj.GetComponent<LobbyHolder>();
                        holder.name = lobby.Name;
                        holder.playerCount = lobby.Players.Count;
                }
            }
            catch(LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
          [ContextMenu("Join test")]
        private async void JoinLobby( )
        {
            try
            {
                QueryResponse response = await Lobbies.Instance.QueryLobbiesAsync();
                await Lobbies.Instance.JoinLobbyByIdAsync(response.Results[0].Id);
                Debug.Log("Joined lobby "+response.Results[0].Name + " id:" +response.Results[0].Id);

            }
            catch (LobbyServiceException e)
            {
               Debug.Log(e);
            }
        }

        
    }
}
