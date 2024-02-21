using Script.UI.Panel;
using UnityEngine;

namespace Script.Characters.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField]
        private LosePanel LosePanel;
        public void Death( ref int currentHealth)
        {
            if (currentHealth <= 0)
                LosePanel.Show();
        }
    }
}
