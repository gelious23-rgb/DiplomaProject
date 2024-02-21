using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class Card : ScriptableObject
{
    public new string name;
    public string description;
    public string cardType;

    public Sprite cardImage;

    public int hp;
    public int attack;
    public int manacost;
}
