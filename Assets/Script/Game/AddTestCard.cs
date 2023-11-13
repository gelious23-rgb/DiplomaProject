using UnityEngine;

public class AddTestCard : MonoBehaviour
{
    public GameObject cardPrefab; // Przypisz prefabrykat karty w Unity Editor.
    public int cardLimit = 6; // Limit kart.

    private int currentCardCount = 1; // Licznik aktualnych kart.

    public void AddCard()
    {
        if (currentCardCount < cardLimit)
        {
            GameObject newCard = Instantiate(cardPrefab, transform); // Tworzy now� kart� w hierarchii obiekt�w Canvas.
                                                                     // Tutaj mo�esz dostosowa� now� kart�, je�li to konieczne.

            // Inkrementacja licznika kart.
            currentCardCount++;
        }
        else
        {
            Debug.LogWarning("Osi�gni�to limit kart. Nie mo�na doda� wi�cej kart.");
        }
    }
}