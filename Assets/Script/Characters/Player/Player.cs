using System;
using UnityEngine;

namespace Script.Characters.Player
{
    public class Player: MonoBehaviour
    {

        
        [SerializeField]private GameObject _playerPrefab;
        [SerializeField]private GameObject _canvasObj;

        private void Awake()
        {
            GameObject player = Instantiate(_playerPrefab, new Vector3(0, -25, 90), Quaternion.identity);
            player.transform.SetParent(_canvasObj.transform,false);
        }

    }
    

}
