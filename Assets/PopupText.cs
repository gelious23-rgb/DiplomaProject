using System;
using System.Collections;
using System.Collections.Generic;
using Script.UI.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    [SerializeField] private DescExplanations descexp;
    [SerializeField]private TextMeshProUGUI text;

     

    public void PopUp(string desc)
    {
        text.text = desc;
        ;
        foreach (var keyword in descexp.Descs)
        {
            
            if (text.text.Contains(keyword))
            {
                text.text += "\n" + keyword + "- " + descexp.Explanations[getExplanationNumber(keyword)];
            }
        }

    }

    private int getExplanationNumber(string keyword)
    {
        switch (keyword)
        {
            case "Execution": return 0;
            break;
            case "Burn": return 1;
            break;
            case "Bleed" : return 2;
            break;
            case "Atk down": return 3;
            break;
            case "Hp down": return 4;
            break;
            case "Blaze": return 5;
                break ;
            case "Miracle": return 6;
                break ;
            case "Elixir": return 7;
                break ;
            case "Blessings": return 8;
                break ;
            case "Transmutation": return 9;
                break ;
            case "Curse": return 10;
                break ;
            case "Wine": return 11;
                break ;
            case "Gluttony": return 12;
                break ;
            case "Wrath": return 13;
                break ;
            case "Pride": return 14;
                break ;
            case "Temperance": return 15;
                break ;
            case "Indignation": return 16;
                break ;
            case "Counterattack": return 17;
                break ;
            case "Protection": return 18;
                break ;
            default: return 0;
        }
        
    }
}
