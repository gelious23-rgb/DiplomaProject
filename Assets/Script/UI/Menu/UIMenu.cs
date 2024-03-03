using System.Collections.Generic;
using Script.Card;
using Script.UI.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Menu
{
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
        [SerializeField] private List<Card.Card> AllCards = new List<Card.Card>();
        private bool IsDeckmode = false;
       [SerializeField] private List<Card.Card> VirDeck = new List<Card.Card>();


        void Start()
        {
  
            foreach (var card in AllCards)
            {
                Spawn(card);
            }

            Scroll = Compendium.transform.GetChild(0).GetComponent<ScrollRect>();
            Scroll_Content = Scroll.content; 

        }

        public void ToggleDeckMode()
        {
            IsDeckmode = !IsDeckmode;
        }

        private void Spawn(Card.Card characterCardSc)
        {
            var card = Instantiate(CardUI, Content.transform);
            var cardSc = card.GetComponent<CardCompendiumSC>();
            cardSc.charCard = characterCardSc;
            cardSc.Name.text = characterCardSc.name;
            cardSc.Desc.text = characterCardSc.description;
            cardSc.Artwork.sprite = characterCardSc.cardImage;
            cardSc.Type.text = characterCardSc.CardType.ToString();
            cardSc.Hp.text = characterCardSc.hp.ToString();
            cardSc.Atk.text = characterCardSc.attack.ToString();
            cardSc.Cost.text = characterCardSc.manacost.ToString();
        
            cardSc.Onclick.onClick.AddListener(()=>OnClickCard(card));
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

            if (IsDeckmode)
            {
                VirDeck.Add(this_.gameObject.GetComponent<CardCompendiumSC>().GetCHCard());
                Debug.Log(this_.gameObject.GetComponent<CardCompendiumSC>().GetCHCard().name+" added to deck");
            }
         
        
        }

       
          [ContextMenu("Make deck file")]
        public void SaveVirDeck()
        {
            SavingHandler.SaveDeck(SavingHandler.CreateDeckFile(VirDeck));
        }

        [ContextMenu("Load deck file")]
        public void LoadVirDeck()
        {
            VirDeck.Clear();
            VirDeck = SavingHandler.LoadDeck().Deck;
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
}
