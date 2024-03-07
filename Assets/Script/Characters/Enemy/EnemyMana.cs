using Script.Game;
using Script.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Characters.Enemy
{
    public class EnemyMana : MonoBehaviour, IMana
    {
        [SerializeField] private int _currentEnemyMana = 1;
        [SerializeField] private TextMeshProUGUI EnemyManaText;
        public int CurrentEnemyMana
        {
            get => _currentEnemyMana;
            set
            {
                _currentEnemyMana = value;
                ShowMana();
            }
        }

        public void ReduceMana(int manaCost)
        {
            _currentEnemyMana = Mathf.Clamp(_currentEnemyMana - manaCost, 0, int.MaxValue);
            ShowMana();
        }
        public void ShowMana() => EnemyManaText.text = CurrentEnemyMana.ToString();
    }
}