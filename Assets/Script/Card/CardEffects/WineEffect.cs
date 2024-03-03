using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Script.Card.CardEffects
{
    public class WineEffect : Effect
    {
        private int PlaceInList;
        private CardInfoDisplay target1;
        private CardInfoDisplay target2;
        private int blessingPower = 1;
        private List<Blessing> _blessings = new List<Blessing>();

        public override void DoOnEnable()
        {
            GetTargets();
            _blessings =ApplyWineBuffs();
        }

        protected override void OnTurnEnd()
        {
            blessingPower++;
            _blessings[0].ATKBlessing = blessingPower;
            _blessings[1].ATKBlessing = blessingPower;
        }

        private void GetTargets()
        {
             PlaceInList = GetCard().owner.Board.FindIndex((x) => x.name == GetCard().name);
             target1 = GetCard().owner.Board[PlaceInList - 1];
             target2 = GetCard().owner.Board[PlaceInList + 1];
             if (PlaceInList == 0)
             {
                 target1 =GetCard().owner.Board[PlaceInList + 2];
             }
        }

        private  List<Blessing> ApplyWineBuffs()
        {
            Blessing wineBless1 = target1.AddComponent<Blessing>();
            wineBless1.ATKBlessing = blessingPower;
            Blessing wineBless2 = target2.AddComponent<Blessing>();
            wineBless2.ATKBlessing = blessingPower;
            wineBless1.ApplyBlessings();
            wineBless2.ApplyBlessings();
            List<Blessing> toReturn = new List<Blessing>();
            toReturn.Add(wineBless1);
            toReturn.Add(wineBless2);
            
            return toReturn;
        }

        private void OnDestroy()
        {
            target1.Heal(3);
            target2.Heal(3);
        }
    }
}
