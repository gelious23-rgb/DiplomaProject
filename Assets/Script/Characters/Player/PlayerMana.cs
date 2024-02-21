using Script.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Characters.Player
{
    public class PlayerMana : MonoBehaviour, IMana
    {
        [SerializeField] private int _currentPlayerMana = 1;
        [SerializeField] private TextMeshProUGUI playerManaText;
        
        public int CurrentPlayerMana
        {
            get => _currentPlayerMana;
            set => _currentPlayerMana = value;
        }

        public void ReduceMana(int manaCost)
        {
            _currentPlayerMana = Mathf.Clamp(_currentPlayerMana - manaCost, 0, int.MaxValue);
            ShowMana();
        }

        public void ShowMana( )
        {
            playerManaText.text = _currentPlayerMana.ToString();
        }
    }
}