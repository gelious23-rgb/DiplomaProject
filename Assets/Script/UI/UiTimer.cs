using System.Collections;
using TMPro;
using UnityEngine;

namespace Script.UI
{
    public class UiTimer: MonoBehaviour
    {
        [SerializeField]private TextMeshProUGUI _turnTimeText;

        private int _turn, _turnTime = 30;
        private TextMeshProUGUI TurnTimeText => _turnTimeText;

        public void ResetTime()
        {
            _turnTime = 30;
            TurnTimeText.text = _turnTime.ToString();
        }

        public IEnumerable NextTurnTime()
        {

            while (_turnTime-- > 0)
            {
                TurnTimeText.text = _turnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }

    }
}
