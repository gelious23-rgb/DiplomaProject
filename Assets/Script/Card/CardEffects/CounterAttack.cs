namespace Script.Card.CardEffects
{
    public class CounterAttack : Effect
    {
        protected override void OnBeingHit(CardInfoDisplay target)
        {
            if (GetCard().IsAlive == true)
            {
                BattleBehaviour.CardsForceFight(GetCard(), target);
            }

        }
    }
}
