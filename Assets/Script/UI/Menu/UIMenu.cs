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
        [SerializeField] private List<Card.CharacterCard> AllCards = new List<Card.CharacterCard>();
    

        void Start()
        {
  
            foreach (var card in AllCards)
            {
                Spawn(card);
            }

            Scroll = Compendium.transform.GetChild(0).GetComponent<ScrollRect>();
            Scroll_Content = Scroll.content; 

        }

        private void Spawn(Card.CharacterCard characterCardSc)
        {
            var card = Instantiate(CardUI, Content.transform);
            var cardSc = card.GetComponent<CardCompendiumSC>();
        
            cardSc.Name.text = characterCardSc.Name;
            cardSc.Desc.text = characterCardSc.Description;
            cardSc.Artwork.sprite = characterCardSc.Image;
            cardSc.Type.text = characterCardSc.Type.ToString();
            cardSc.Hp.text = characterCardSc.Hp.ToString();
            cardSc.Atk.text = characterCardSc.Damage.ToString();
            cardSc.Cost.text = characterCardSc.Manacost.ToString();
        
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
