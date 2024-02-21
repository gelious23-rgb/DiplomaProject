using System;
using System.Collections;
using System.Collections.Generic;
using Script.UI.Text;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    [SerializeField] private DescExplanations descexp;
    [SerializeField]private TextMeshProUGUI text;

     

    public void PupUP()
    {
        for (var index = 0; index < descexp.Descs.Length; index++)
        {
            var Keyword = descexp.Descs[index];
            if (text.text.Contains(Keyword))
            {
               // Debug.Log("contains keyword" + " " +Keyword);
               text.text = text.text.Replace(text.text, text.text + "\n " + Keyword +
                                             " - " + descexp.Explanations[index] + "\n");
            }
        }
       
    }
}
