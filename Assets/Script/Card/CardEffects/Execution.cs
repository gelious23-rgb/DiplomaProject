using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Card.CardEffects
{
     public class Execution : Effect
     {
          private GameObject BuffIcon;
          
          public override void DoOnEnable()
          {
               BuffIcon = GetCard().BuffSpriteSpace.transform.GetChild(2).gameObject;
               BuffIcon.SetActive(true);

          }

          public override void OnAttack(CardInfoDisplay self, CardInfoDisplay target)
          {
               Debug.Log("Self is " +self.CharacterCard.name);
               Debug.Log("Target is " +target.CharacterCard.name);
               Debug.Log("Execution check " + "\n" + "Target hp must be less than "
                         + Mathf.RoundToInt(target.CharacterCard.hp * 0.5f));
               if (target.CurrentHP <= Mathf.RoundToInt(target.CharacterCard.hp * 0.5f))
               {
                    BattleBehaviour._calculateDamage.DealDamageToCharacterDirectly(target.OwnerHp,
                         target.CharacterCard.manacost);
                    BattleBehaviour.CardDeath.DestroyCard(target);
                    Debug.Log("Executed "+target.CharacterCard.name);
                    

                    Debug.Log(target.OwnerHp +  "suffered " +target.CharacterCard.manacost + " damage" );
                   
               }
          }
          
          protected override void OnTurnEnd()
          {
               base.OnTurnEnd();
          }

     }
}
