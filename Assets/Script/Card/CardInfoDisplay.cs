using System;
using Script.Card.CardEffects;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Card
{
   public class CardInfoDisplay : MonoBehaviour
    {

        public Card CharacterCard;
        [SerializeField]
        private Image _sprite;

        [SerializeField]
        private TextMeshProUGUI _name;

        [SerializeField] public TextMeshProUGUI _description;

        [SerializeField]
        private TextMeshProUGUI _type;

        [SerializeField]
        private TextMeshProUGUI _attack, _hp, _manacost;
        
        [SerializeField]
        private GameObject _hideGO,_highliter;
        public bool IsPlayer;
        [SerializeField]
        private Color  _targetColor;
        [SerializeField]
        private Color _normalColor;
        public Image _Image;
        public bool CanAttack;
        public bool IsPlaced;
<<<<<<< Updated upstream
       [HideInInspector] public int HP;
       [HideInInspector] public int ATK;
      [HideInInspector] public int DamageResistance = 0;
=======
        public int HP;
        [HideInInspector] public int ATK; 
        [HideInInspector] public int DamageResistance = 0; 
        public IHealth owner; 
        public GameObject BuffSpriteSpace;
>>>>>>> Stashed changes
        public bool IsAlive => HP > 0;

        private void Start()
        {
           
            CardEffectHandler.OnTurnStart.AddListener(OnTurnStart);
<<<<<<< Updated upstream
            CardEffectHandler.OnTurnStart.AddListener(test);
        }
           [ContextMenu("force start")]
        private void OnTurnStart()
        {
            AddPassive(CharacterCard.GetCardType());
        }
=======
           
        }
           [ContextMenu("force start")]
           internal void OnTurnStart()
         {
            AddPassive(CharacterCard.GetCardType());
            foreach (var effect in CharacterCard.Effects)
            {
               var actualEffect = effect.GetComponent<Effect>();
               if(!CheckIfHasPassive(gameObject, actualEffect)){gameObject.AddComponent(actualEffect.GetType());}
            }
         }

           private bool CheckIfHasPassive(GameObject obj, Effect passive)
           {
               return obj.TryGetComponent(passive.GetType(), out Component _); 
           }
>>>>>>> Stashed changes

        private void test()
        {
            Debug.Log("Test success");
        }

        private void AddPassive(Card.Types Name)
        {
            switch (Name)
            {
                case Card.Types.Man:
                    if (!CheckIfHasPassive(gameObject, new CounterAttack()))
                    {
                        gameObject.AddComponent<CounterAttack>();
                        
                    }
                    break;
                case Card.Types.Powers:
                    if (!CheckIfHasPassive(gameObject, new Protection()))
                    {
                        gameObject.AddComponent<Protection>();
                        
                    }
                    break;
            }
        }

        public void ChangeAttackState(bool canAttack)
        {
            CanAttack = canAttack;
        }

        public virtual void TakeDamage(int dmg, CardInfoDisplay damageSource)
        {
            
            if (DamageResistance > dmg)
            {
                DamageResistance = dmg;
            }
            Debug.Log(this.CharacterCard.name +" prot is "+DamageResistance);
            Debug.Log(dmg-DamageResistance + " damage taken  by " + this.CharacterCard.name);
            HP -= dmg-DamageResistance;
        }

        public void ShowCardInfo(Card characterCard, bool isPlayer)
        {
            IsPlayer = isPlayer;
            _hideGO.SetActive(false);

            CharacterCard = characterCard;
            HP = characterCard.hp;
            ATK = characterCard.attack;

            _sprite.sprite = characterCard.cardImage;
            _sprite.preserveAspect = true;
            _name.text = characterCard.name;
            _description.text = characterCard.description;
            _type.text = characterCard.CardType.ToString();
        
        

            RefreshData();
        }

        public void HideCardInfo(Card characterCard)
        {
            CharacterCard = characterCard;
            _hideGO.SetActive(true);
            IsPlayer = false;
        }

        public void RefreshData()
        {
            _attack.text = ATK.ToString();
            _hp.text = HP.ToString();
            _manacost.text = CharacterCard.manacost.ToString();
            
        }



        
        public void HighlightCard() => _highliter.SetActive(true);

        public void DeHighlightCard() => _highliter.SetActive(false);

        public void CheckForAvailability(int currentMana)
        {
            if (GetComponent<CanvasGroup>() == null)
            {
                Debug.Log("_canvasGroup == null");
            }
            else
            {
                GetComponent<CanvasGroup>().alpha = currentMana >= CharacterCard.manacost ? 1 : .5f;
            }

        }

        public void HighlightAsTarget(bool highlight) => _Image.color = highlight ? _targetColor : _normalColor;

    }
}

