using Script.UI.Panel;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Characters.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {

        [SerializeField]
        private WinPanel _winPanel;
        public void Death( ref int currentHealth)
        {
            if (currentHealth <= 0)
                _winPanel.Show();
        }
    }
}
