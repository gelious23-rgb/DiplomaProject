using Script.UI.Panel;
using UnityEngine;

namespace Script.Characters.Player
{
    public class PlayerDeath : MonoBehaviour, ICharacterDeath
    {
        [SerializeField] private WinPanel WinPanel;
        public void Death( int currentHealth)
        {
            if (currentHealth <= 0)
                WinPanel.Show();
        }
    }
}
