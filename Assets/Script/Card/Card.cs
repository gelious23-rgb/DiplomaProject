using Script.Card.CardEffects;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
    public class Card : ScriptableObject
    {
        public new string name;
        [TextArea]  public string description;

        public  Types CardType; 

        public Sprite cardImage;

        public int hp;
        public int attack;
        public int manacost;
        [System.Serializable]
        public enum Types
        {
            Tool, Man, Powers, Relic, Heroic
        };

        private void Start()
        {
            CardEffectHandler.OnTurnStart.AddListener(OnTurnStart);
        }
        
        void OnTurnStart()
        {

            switch (CardType)
            { 
                case Types.Tool:
                    break;
                case Types.Man:
                    Debug.Log("CounterAttack added");
                    this.AddComponent<CounterAttack>();
                    break;
                case Types.Heroic:
                    break;
                case Types.Powers:
                    break;
                case Types.Relic:
                    break;

            }
        }
    }
}
