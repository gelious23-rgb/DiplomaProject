using System.Collections;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace Script.UI
{
    public class UiTimer: NetworkBehaviour
    {
        [SerializeField]private TextMeshProUGUI _turnTimeText;

        private NetworkVariable<int> _turnTime = new(30);
        private TextMeshProUGUI TurnTimeText => _turnTimeText;

        public override void OnNetworkSpawn() => _turnTime.OnValueChanged += OnValueChanged;
        private void OnValueChanged(int previousvalue, int newvalue) => TurnTimeText.text = newvalue.ToString();

        public void ResetTime()
        {
            _turnTime.Value = 100;
            TurnTimeText.text = _turnTime.ToString();
        }

        public IEnumerable NextTurnTime()
        {
            while (_turnTime.Value --> 0)
            {
                yield return new WaitForSeconds(1);
            }
        }

    }
}
