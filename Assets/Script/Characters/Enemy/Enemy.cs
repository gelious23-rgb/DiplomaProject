using UnityEngine;

namespace Script.Characters.Enemy
{
    public class Enemy : MonoBehaviour
    {

        
        [SerializeField]private GameObject _enemyPrefab;
        [SerializeField]private GameObject _canvasObj;
        private void Awake()
        {
            GameObject player = Instantiate(_enemyPrefab, new Vector3(0, -25, 90), Quaternion.identity) as GameObject;
            player.transform.SetParent(_canvasObj.transform,false);
        }
    }
}
