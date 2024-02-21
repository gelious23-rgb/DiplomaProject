using Script.Logic;
using TMPro;
using UnityEngine;

namespace Script.Characters.Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private TextMeshProUGUI PlayerHealthText;
        [SerializeField] private int _currentHealth;

        public int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }


        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            Show();
        }

        public void Show() => PlayerHealthText.text = CurrentHealth.ToString();

    }
}
