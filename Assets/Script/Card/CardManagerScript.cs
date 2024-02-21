using UnityEngine;

namespace Script.Card
{
    public class CardManagerScript : MonoBehaviour
    {
        private void Awake()
        {
            CardInstance.AllCards.Add(new CharacterCard("Bael","Powers","Gluttony","Sprite/Cards/Bael",5,3,6));
            CardInstance.AllCards.Add(new CharacterCard("Beast", "Heroic", "All cards on the field receive 1 damage when it is played. If alive for  3 turns, kills one enemy card every turn.", "Sprite/Cards/Beast",6,5,11));
            CardInstance.AllCards.Add(new CharacterCard("Crown Of Thorns", "Relic", "Grants 2 hp Blessings for all allies. Every time ally receives damage, restore 1 hp for that ally and increase its attack by +1. If ally dies when crown is on the field, draw 1 card", "Sprite/Cards/Crown Of Thorns",0,5,10));
            CardInstance.AllCards.Add(new CharacterCard("Fanatic", "Man", "Indignation", "Sprite/Cards/Fanatic", 2, 2, 2));
            CardInstance.AllCards.Add(new CharacterCard("Holy Grail", "Relic", "Grants Elixir to all allies every turn. If alive for 3 turns, also grant 2 blessings for atk&hp. Wine. ", "Sprite/Cards/Holy Grail", 6, 6, 12));
            CardInstance.AllCards.Add(new CharacterCard("Imp", "Powers", "Protection. When attacking, target’s attack is reduced by 1 until the next turn.", "Sprite/Cards/Imp", 2, 3, 3));
            CardInstance.AllCards.Add(new CharacterCard("Longinus", "Heroic", "Counterattack. When destroying a card, every ally receives +1 atk +1hp. On death, transform into “Holy lance”", "Sprite/Cards/Longinus", 3, 3, 8));
            CardInstance.AllCards.Add(new CharacterCard("Martyr", "Man", "Counterattack. When hit, additionally deal 1 damage to an attacker even if received lethal damage. ", "Sprite/Cards/Martyr", 1, 1, 4));
            CardInstance.AllCards.Add(new CharacterCard("Murder", "Man", "Counterattack. Every time any card is destroyed, increase own attack by 1", "Sprite/Cards/Murder", 2, 1, 3));
            CardInstance.AllCards.Add(new CharacterCard("Ophanim", "Powers", "Protection. Blaze ", "Sprite/Cards/Ophanim", 3, 5, 7));
            CardInstance.AllCards.Add(new CharacterCard("Seraph", "Powers", "Execution", "Sprite/Cards/Seraph", 6, 4, 6));
            CardInstance.AllCards.Add(new CharacterCard("Sisyphus", "Relic", "Upon death, revive at the cost of death of one Powers card ", "Sprite/Cards/Sisyphus", 5, 5, 8));
            CardInstance.AllCards.Add(new CharacterCard("The Lamb", "Heroic", "Pride. If alive for 3 turns, deal 30% hp to it’s owner and opponent.", "Sprite/Cards/The Lamb", 3, 7, 22));

        }
    }
}