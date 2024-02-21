using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private Player activePlayer;
    private int turnCount;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        turnCount = 1;
        activePlayer = GetStartingPlayer();
        StartTurn();
    }

    private void StartTurn()
    {
        Debug.Log("Turn " + turnCount + ": " + activePlayer.Name);
        activePlayer.StartTurn();
    }

    public void EndTurn()
    {
        activePlayer.EndTurn();
        turnCount++;
        activePlayer = GetNextPlayer();
        StartTurn();
    }

    private Player GetStartingPlayer()
    {
        // Implement your logic to determine the starting player
        return null;
    }

    private Player GetNextPlayer()
    {
        // Implement your logic to get the next player based on turn order
        return null;
    }
}
