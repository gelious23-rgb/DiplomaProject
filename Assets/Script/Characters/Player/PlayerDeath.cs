using Script.UI.Panel;
using UnityEngine;


namespace Script.Characters.Player
{
    public class PlayerDeath : MonoBehaviour, ICharacterDeath
    {

        [SerializeField] private LosePanel _losePanel;
        public void Death( int currentHealth)
        {
            if (currentHealth <= 0)
                _losePanel.Show();
        }
    }
}
