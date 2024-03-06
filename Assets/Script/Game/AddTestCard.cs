using UnityEngine;

public class AddTestCard : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private int cardLimit = 6;

    private int currentCardCountPlayer = 1;
    private int currentCardCountEnemy = 1;

    public void AddCard()
    {
        if (currentCardCountPlayer < cardLimit)
        {
            GameObject newCard = Instantiate(cardPrefab, transform); 

            // Inkrementacja licznika kart.
            currentCardCountPlayer++;
        }
        else
        {
            Debug.LogWarning("Osi�gni�to limit kart. Nie mo�na doda� wi�cej kart.");
        }
    }
}