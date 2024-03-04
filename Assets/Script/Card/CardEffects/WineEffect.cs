using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class WineEffect : Effect
    {
        private int blessingPower = 1;
        private List<Blessing> _blessings = new List<Blessing>();
       [SerializeField] private int _counter = 2;

        public override void DoOnEnable()
        {
            _counter = 2;
        }

        protected override void OnTurnEnd()
        {
            if (GetCard().IsPlaced)
            {
                if (_blessings != null)
                {
                    foreach (var blessing in _blessings)
                    {
                        blessing.ATKBlessing += 1;
                        blessing.HpBlessing += 1;
                        blessing.ApplyBlessings();
                    }
                }
            }
        }

        public override void OnBeingPlayed(CardInfoDisplay self)
        {
            if (self == GetCard() )
            {
                var board = GetCard().owner.Board;
                if (board.Count == 1)
                {
                    _counter = 1;
                }
                if (board != null)
                {
                    foreach (var card in board)
                    {
                        if (card != self)
                        {
                            if (_counter > 0)
                            {
                                var bless = card.AddComponent<Blessing>();
                                bless.HpBlessing = 2;
                                bless.ATKBlessing = 1;
                                bless.ApplyBlessings();
                                _blessings.Add(bless);
                                _counter--;
                            }
                        }
    
                    }
 
                }
            }
            if (GetCard().IsPlaced && self.owner == GetCard().owner && self != GetCard())
            {
                Debug.Log("Ally Card not Self detected");
                if (_counter > 0)
                {
                    var bless = self.AddComponent<Blessing>();
                    bless.HpBlessing = 2;
                    bless.ATKBlessing = 1;
                    bless.ApplyBlessings();
                    _blessings.Add(bless);
                    _counter--;
                }


            }
        }




        private void OnDestroy()
        {
            foreach (var blessing in _blessings )
            {
                blessing.GetCard().Heal(3);
            }
        }
    }
}
