using TMPro;
using UnityEngine;
using EasyButtons;
using Script.Characters;

namespace Script.Health
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField]
        private int _hpValue;
        private TextMeshProUGUI _hp;
        private Player _player;

        public int HpValue
        {
            get { return _hpValue; }
        }

        private void Awake()
        {
            _player = GetComponentInParent<Player>();
            _hp = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            _hp.text = _hpValue.ToString();
        }
        
        [Button]
        public void Decrease(int decreaseValue)
        {
            _hpValue -= decreaseValue;
            _hp.text = _hpValue.ToString();
            
            

        }
    }
}
