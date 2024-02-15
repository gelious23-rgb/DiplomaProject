using UnityEngine;
using TMPro;

public class RoomCodeGenerator : MonoBehaviour
{
    public TextMeshProUGUI codetext;

    void Start()
    {
        ChangeText();
    }

    public void ChangeText()
    {
        string roomCode = GenerateRandomString(4);
        codetext.text = roomCode;
    }

    private string GenerateRandomString(int length)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        System.Random random = new System.Random();
        char[] result = new char[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = characters[random.Next(characters.Length)];
        }

        return new string(result);
    }

}
