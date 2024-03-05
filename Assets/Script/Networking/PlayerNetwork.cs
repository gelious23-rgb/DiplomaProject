using Script.Characters.Enemy;
using Script.Characters.Player;
using Unity.Netcode;


namespace Script.Networking
{
    public class PlayerNetwork : NetworkBehaviour
    {

        private PlayerHealth _playerHealth; 
        private PlayerMana _playerMana;
        private PlayerDeath _playerDeath;
        

        private EnemyHealth _enemyHealth; 
        private EnemyMana _enemyMana; 
        private EnemyDeath _enemyDeath;
        
        private void Awake()
        {
            if (IsLocalPlayer)
            {
                _enemyDeath = gameObject.AddComponent<EnemyDeath>();
                _enemyHealth = gameObject.AddComponent<EnemyHealth>();
                _enemyMana = gameObject.AddComponent<EnemyMana>();
            }
            else
            {
                _playerDeath = gameObject.AddComponent<PlayerDeath>();
                _playerHealth = gameObject.AddComponent<PlayerHealth>();
                _playerMana = gameObject.AddComponent<PlayerMana>();

            }
        }
        
    }
}
