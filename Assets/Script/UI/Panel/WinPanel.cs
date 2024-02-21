using EasyButtons;
using Script.UI.Buttons;

using TMPro;
using UnityEngine;

namespace Script.UI.Panel
{
    public class WinPanel: MonoBehaviour , IPanel
    {
        [SerializeField]
        private TextMeshProUGUI _playerGameOver;
        [SerializeField]
        private RestartButton _restartTurnButton;
        
        [Button]
        public void Show()
        {
            Time.timeScale = 0;
            _playerGameOver.gameObject.SetActive(true);
            _restartTurnButton.gameObject.SetActive(true);
        }
    }
}