using Script.Logic;
using TMPro;
using UnityEngine;

namespace Script.Characters.Enemy
{
    public class EnemyHealth : MonoBehaviour,IHealth
    {
        [SerializeField] private  TextMeshProUGUI _enemyHealthText;
        [SerializeField] private int _currentHealth;
        
        public int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            Show();
        }
        public void Show()
        {
            
            _enemyHealthText.text = _currentHealth.ToString();
        }
    }
}
