using UnityEngine;

namespace Script.Card.CardEffects
{
     public class Execution : Effect
     {
          public override void OnAttack(CardInfoDisplay target, CardInfoDisplay self)
          {
               if (target.HP <= Mathf.RoundToInt(target.CharacterCard.hp * 0.5f))
               {
                    BattleBehaviour.CardDeath.DestroyCard(target);
                    Debug.Log("Executed "+target.CharacterCard.name);
                    BattleBehaviour._calculateDamage.DealDamageToCharacterDirectly(target.owner,
                         target.CharacterCard.manacost);
                    Debug.Log(target.owner +  "suffered " +target.CharacterCard.manacost + " damage" );
                   
               }
          }
     }
}
