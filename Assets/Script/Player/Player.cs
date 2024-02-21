using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name { get; private set; }
    public int Resources { get; private set; }

    public void Initialize(string name)
    {
        Name = name;
        // Set initial resources or any other necessary setup
    }

    public void StartTurn()
    {
        // Perform any actions specific to the start of the turn
        // Generate resources or mana, draw cards, etc.
    }

    public void EndTurn()
    {
        // Perform any cleanup or end-of-turn actions
        // Reset resources, check for end-of-turn effects, etc.
    }


}
