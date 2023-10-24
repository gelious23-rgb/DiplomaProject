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

    public Card(string p_name,string p_type, string p_description, string p_spritePath, int p_damage, int p_manacost, int p_hp)
    {
        name = p_name;
        type = p_type;
        description = p_description;
        image = Resources.Load<Sprite>(p_spritePath);
        damage = p_damage;
        manacost = p_manacost;
        hp = p_hp;
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

    }
}
