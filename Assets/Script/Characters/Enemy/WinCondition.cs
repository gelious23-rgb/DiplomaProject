using Script.UI.Panel;
using UnityEngine;

namespace Script.Characters.Enemy
{
    public class WinCondition : MonoBehaviour
    {

        [SerializeField] private WinPanel WinPanel;
        public void Death( ref int currentHealth)
        {
            if (currentHealth <= 0)
                WinPanel.Show();
        }
    }
}
