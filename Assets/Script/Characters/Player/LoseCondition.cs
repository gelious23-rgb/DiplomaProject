using Script.UI.Panel;
using UnityEngine;

namespace Script.Characters.Player
{
    public class LoseCondition : MonoBehaviour, ICharacterDeath
    {
        [SerializeField]
        private LosePanel LosePanel;
        public void Death(  int currentHealth)
        {
            if (currentHealth <= 0)
                LosePanel.Show();
        }

    }
}
