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
            GameObject newCard = Instantiate(cardPrefab, transform); // Tworzy now¹ kartê w hierarchii obiektów Canvas.
                                                                     // Tutaj mo¿esz dostosowaæ now¹ kartê, jeœli to konieczne.

            // Inkrementacja licznika kart.
            currentCardCount++;
        }
        else
        {
            Debug.LogWarning("Osi¹gniêto limit kart. Nie mo¿na dodaæ wiêcej kart.");
        }
    }
}