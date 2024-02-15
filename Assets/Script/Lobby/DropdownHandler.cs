using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DropdownHandler : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown1;
    [SerializeField] private TMP_Dropdown dropdown2;
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private Sprite angelBorder;
    [SerializeField] private Sprite demonBorder;
    [SerializeField] private Image Player1Frame;
    [SerializeField] private Image Player2Frame;


    private void Start()
    {
        // Przypisanie metod do zdarzeñ OnValueChanged dla obu dropdownów
        dropdown1.onValueChanged.AddListener(OnDropdown1ValueChanged);
        dropdown2.onValueChanged.AddListener(OnDropdown2ValueChanged);
        dropdown2.value = 2;
    }

    private void OnDropdown1ValueChanged(int index)
    {
        // Sprawdzenie wybranej opcji w dropdownie 1
        string selectedOption = dropdown1.options[index].text;

        image1.color = (selectedOption == "Angels") ? Color.blue : Color.red;

        if (selectedOption == "Angels")
        {
            Image dropdownImage = dropdown1.GetComponent<Image>();
            dropdownImage.sprite = angelBorder;
            Player1Frame.sprite = angelBorder;

        }
        else if (selectedOption == "Demons")
        {
            Image dropdownImage = dropdown1.GetComponent<Image>();
            dropdownImage.sprite = demonBorder;
            Player1Frame.sprite = demonBorder;
        }
    }

    private void OnDropdown2ValueChanged(int index)
    {
        // Sprawdzenie wybranej opcji w dropdownie 2
        string selectedOption = dropdown2.options[index].text;

        image2.color = (selectedOption == "Angels") ? Color.blue : Color.red;

        if (selectedOption == "Angels")
        {
            Image dropdownImage = dropdown2.GetComponent<Image>();
            dropdownImage.sprite = angelBorder;
            Player2Frame.sprite = angelBorder;
        }
        else if (selectedOption == "Demons")
        {
            Image dropdownImage = dropdown2.GetComponent<Image>();
            dropdownImage.sprite = demonBorder;
            Player2Frame.sprite = demonBorder;
        }
    }
}
