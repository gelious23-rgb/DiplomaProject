using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 
using UnityEngine.Events;


public class UIMenu : MonoBehaviour
{
   [HideInInspector] [SerializeField] private DescExplanations descexp;
    
   // [SerializeField] private Button[] yesNo; //всплывающее окно на будущее
       private GameObject NowOpened;
    [SerializeField] private GameObject Compendium;
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject CardUI;
    [SerializeField] private TextMeshProUGUI NameUI;
    [SerializeField] private TextMeshProUGUI DescUI;
    private ScrollRect Scroll;
    private RectTransform Scroll_Content;
   [SerializeField] private List<Card> AllCards = new List<Card>();
    

    void Start()
    {
  
        foreach (var card in AllCards)
        {
            Spawn(card);
        }

        Scroll = Compendium.transform.GetChild(0).GetComponent<ScrollRect>();
        Scroll_Content = Scroll.content; 

    }

    private void Spawn(Card cardSC)
    {
        var card = Instantiate(CardUI, Content.transform);
        var cardSc = card.GetComponent<CardCompendiumSC>();
        
        cardSc.name_.text = cardSC.name;
        cardSc.desc.text = cardSC.description;
        cardSc.artwork.sprite = cardSC.cardImage;
        cardSc.Type.text = cardSC.CardType.ToString();
        cardSc.hp.text = cardSC.hp.ToString();
        cardSc.atk.text = cardSC.attack.ToString();
        cardSc.cost.text = cardSC.manacost.ToString();
        
        cardSc.onclick.onClick.AddListener(()=>OnClickCard(card));
    }

    private void OnClickCard(GameObject this_)
    {
        NameUI.text = this_.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        DescUI.text = this_.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        for (var index = 0; index < descexp.Descs.Length; index++)
        {
            var Keyword = descexp.Descs[index];
            if (DescUI.text.Contains(Keyword))
            {
                DescUI.text = DescUI.text.Replace(DescUI.text, DescUI.text + "\n " + Keyword +
                                                               " - " + descexp.Explanations[index]+ "\n");
            }
        }
         
        
    }

    
    
    
 public  void Open(GameObject what)
    {
        Close(NowOpened);
        NowOpened = what;
        what.SetActive(true);
    }

    public void Close(GameObject _this)
    {
        if (_this == null)
        {
            return;
        }
        _this.SetActive(false);
        _this = null;
    }
    public void ActExitGame()
    {
        Application.Quit();
    }
    
    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape))
       {
          Close(NowOpened);
       }

       

    }
}
