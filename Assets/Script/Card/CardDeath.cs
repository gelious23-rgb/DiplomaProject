using Script.Spawner;
using UnityEngine;

namespace Script.Card
{
    public class CardDeath : MonoBehaviour
    {   
        [SerializeField]
        private PlayerSpawnerCards _playerSpawnerCards;
        [SerializeField]
        private EnemySpawnerCards _enemySpawnerCards;

        public void DestroyCard(CardInfoDisplay card)
        {

                card.GetComponent<CardMove>().OnEndDrag(null);

                if (_enemySpawnerCards.EnemyFieldCards.Exists(x => x == card))
                    _enemySpawnerCards.EnemyFieldCards.Remove(card);

                if (_playerSpawnerCards.PlayerFieldCards.Exists(x => x == card))
                    _playerSpawnerCards.PlayerFieldCards.Remove(card);

                Destroy(card.gameObject);
                
        }
    }
}