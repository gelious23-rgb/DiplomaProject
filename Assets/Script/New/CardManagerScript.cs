using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Card
{
    public string name;
    public string type;
    public string description;
    public int damage, manacost, hp;
    public Sprite image;

    public bool canAttack;

    public bool isAlive
    {
        get { return hp > 0; }
    }

    public Card(string p_name,string p_type, string p_description, string p_spritePath, int p_damage, int p_manacost, int p_hp)
    {
        name = p_name;
        type = p_type;
        description = p_description;
        image = Resources.Load<Sprite>(p_spritePath);
        damage = p_damage;
        manacost = p_manacost;
        hp = p_hp;
        canAttack = false;
    }

    public void ChangeAttackState(bool p_canAttack)
    {
        canAttack = p_canAttack;
    }

    public void GetDamage(int dmg)
    {
        hp -= dmg;
    }
}

public static class CardManager
{
    public static List<Card> AllCards = new List<Card>();
}

public class CardManagerScript : MonoBehaviour
{
    private void Awake()
    {
        CardManager.AllCards.Add(new Card("Bael","Powers","Gluttony","Sprite/Cards/Bael",5,3,6));
        CardManager.AllCards.Add(new Card("Beast", "Heroic", "All cards on the field receive 1 damage when it is played. If alive for  3 turns, kills one enemy card every turn.", "Sprite/Cards/Beast",6,5,11));
        CardManager.AllCards.Add(new Card("Crown Of Thorns", "Relic", "Grants 2 hp Blessings for all allies. Every time ally receives damage, restore 1 hp for that ally and increase its attack by +1. If ally dies when crown is on the field, draw 1 card", "Sprite/Cards/Crown Of Thorns",0,5,10));
        CardManager.AllCards.Add(new Card("Fanatic", "Man", "Indignation", "Sprite/Cards/Fanatic", 2, 2, 2));
        CardManager.AllCards.Add(new Card("Holy Grail", "Relic", "Grants Elixir to all allies every turn. If alive for 3 turns, also grant 2 blessings for atk&hp. Wine. ", "Sprite/Cards/Holy Grail", 6, 6, 12));
        CardManager.AllCards.Add(new Card("Imp", "Powers", "Protection. When attacking, target’s attack is reduced by 1 until the next turn.", "Sprite/Cards/Imp", 2, 3, 3));
        CardManager.AllCards.Add(new Card("Longinus", "Heroic", "Counterattack. When destroying a card, every ally receives +1 atk +1hp. On death, transform into “Holy lance”", "Sprite/Cards/Longinus", 3, 3, 8));
        CardManager.AllCards.Add(new Card("Martyr", "Man", "Counterattack. When hit, additionally deal 1 damage to an attacker even if received lethal damage. ", "Sprite/Cards/Martyr", 1, 1, 4));
        CardManager.AllCards.Add(new Card("Murder", "Man", "Counterattack. Every time any card is destroyed, increase own attack by 1", "Sprite/Cards/Murder", 2, 1, 3));
        CardManager.AllCards.Add(new Card("Ophanim", "Powers", "Protection. Blaze ", "Sprite/Cards/Ophanim", 3, 5, 7));
        CardManager.AllCards.Add(new Card("Seraph", "Powers", "Execution", "Sprite/Cards/Seraph", 6, 4, 6));
        CardManager.AllCards.Add(new Card("Sisyphus", "Relic", "Upon death, revive at the cost of death of one Powers card ", "Sprite/Cards/Sisyphus", 5, 5, 8));
        CardManager.AllCards.Add(new Card("The Lamb", "Heroic", "Pride. If alive for 3 turns, deal 30% hp to it’s owner and opponent.", "Sprite/Cards/The Lamb", 3, 7, 22));

    }
}
