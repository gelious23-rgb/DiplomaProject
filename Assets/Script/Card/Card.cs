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

<<<<<<< HEAD
        public void OnStart()
        {
             
            
        }
        
       public Types GetCardType()
=======
        private void Start()
        {
            CardEffectHandler.OnTurnStart.AddListener(OnTurnStart);
        }
        
        void OnTurnStart()
>>>>>>> 6af6e68b54f96baeaf263203283ac3c9dbdd654d
        {

            switch (CardType)
            { 
                case Types.Tool:
<<<<<<< HEAD
                    return Types.Tool;
                    break;
                case Types.Man:
                    return Types.Man;
                    break;
                case Types.Heroic:
                    return Types.Heroic;
                    break;
                case Types.Powers:
                    return Types.Powers;
                    break;
                case Types.Relic:
                    return Types.Relic;
                    break;

            }

            return CardType;
=======
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
>>>>>>> 6af6e68b54f96baeaf263203283ac3c9dbdd654d
        }
    }
}
