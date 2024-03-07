using System.Collections.Generic;
using Script.Card;
using Script.UI.Menu;
using Script.UI.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script.Lobby
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject MenuPanel;
        [SerializeField] private GameObject JoinPanel;
        [SerializeField] private GameObject SettingsPanel;
        [SerializeField] private GameObject Lobby;
        [SerializeField] private List<Card.Card> AllCards = new List<Card.Card>();
        private bool IsDeckmode = false;
        private ScrollRect Scroll;
        private RectTransform Scroll_Content; 
        [SerializeField] private List<Card.Card> VirDeck = new List<Card.Card>();
        [SerializeField] private GameObject CardUI;
        [SerializeField] private TextMeshProUGUI NameUI;
        [SerializeField] private TextMeshProUGUI DescUI;
        [SerializeField] private GameObject Compendium;
        [SerializeField] private GameObject Content;
        [SerializeField] private DescExplanations descexp;
        private GameObject NowOpened;
        void Start()
        {
  
            foreach (var card in AllCards)
            {
                Spawn(card);
            }

            Scroll = Compendium.transform.GetChild(0).GetComponent<ScrollRect>();
            Scroll_Content = Scroll.content; 

        }
        public  void Open(GameObject what)
        {
            Close(NowOpened);
            NowOpened = what;
            what.SetActive(true);
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Close(NowOpened);
            }

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


        public void CreateGame()
        {
           Open(Lobby);
        }
        
        public void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
        public void FindGame()
        {
            Open(JoinPanel);
            //Close(MenuPanel);
        }
        public void JoinGame()
        {
            //Join Game script
            Debug.Log("Join game");
        }
        public void BacktoLobby()
        {
            //JoinPanel.SetActive(false);
            Open(MenuPanel);
        }

        public void SettingsButton()
        {
            //MenuPanel.SetActive(false);
            Open(SettingsPanel) ;
        
        }
        public void BackSettings()
        {
           // SettingsPanel.SetActive(false);
            Open(MenuPanel);
        }
        public void Compedium()
        {
            Open(Compendium);
            
        }
        public void RunSinglePlayer()
        {
            SceneManager.LoadScene(2);
        }

    }
}
