
using System;
using Script.Card.CardEffects;
using Script.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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

        private int _maxHp;

        public int MaxHp
        {
            get
            {
                var blessing = GetComponent<Blessing>();
                if (blessing == null || blessing.HpBlessing == 0)
                {
                    Debug.Log("No blessings, returning normal value: " + _maxHp);
                    return _maxHp;
                }
                else if (blessing!=null && blessing.HpBlessing != 0)
                {
                    _maxHp = _maxHp+ blessing.HpBlessing;
                    Heal(blessing.HpBlessing);
                    Debug.Log("Hp blessing = "+blessing.HpBlessing + " MaxHp = "+ _maxHp);
                    return _maxHp;
                }
                else
                {
                    return _maxHp;
                }
                
            }
            set
            {
                var blessing = GetComponent<Blessing>();
                if (blessing != null && blessing.HpBlessing != 0)
                {
                    _maxHp = value + blessing.HpBlessing;
                    RefreshData();
                }
                else
                {
                    
                    _maxHp = value;
                }
                
            }
        }

         [FormerlySerializedAs("HP")] public int CurrentHP;
      public int ATK;
       public int DamageResistance = 0;
      public IHealth owner;
      public GameObject BuffSpriteSpace;
        public bool IsAlive => CurrentHP > 0;

        internal void Start()
        {
           
            CardEffectHandler.OnTurnStart.AddListener(OnTurnStart);
            MaxHp = CurrentHP;

        }

        public int Heal(int healAmount) //returns OverHeal amount
        {
            var healValue = CurrentHP +healAmount;
            if (healValue > _maxHp)
            {
                CurrentHP = _maxHp;
                RefreshData();
                return healValue - _maxHp;
            }
            else
            {
                CurrentHP += healAmount;
                RefreshData();
                return 0;
            }
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
               if (passive.GetType() == typeof(Blessing))
               {
                   return false;
               }
               else
               {
                   return obj.TryGetComponent(passive.GetType(), out Component _);
               }
                
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
            CurrentHP -= dmg-DamageResistance;
        }

        public void ShowCardInfo(Card characterCard, bool isPlayer)
        {
            IsPlayer = isPlayer;
            _hideGO.SetActive(false);

            CharacterCard = characterCard;
            CurrentHP = characterCard.hp;
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
            _hp.text = CurrentHP.ToString();
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
