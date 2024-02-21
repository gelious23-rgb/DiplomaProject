using Script.Game;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;


namespace Script.UI.Buttons
{
    public class EndTurnButton : MonoBehaviour
    {
        [SerializeField]
        private Button _endTurnButton;
        
        public Button EndTurn => _endTurnButton;



    }
}