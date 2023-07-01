using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Card
{
    public class CardCompendiumSC : MonoBehaviour
    {
        public Button Onclick;
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Type, Desc;
        public TextMeshProUGUI Hp;
        public TextMeshProUGUI Atk;
        public TextMeshProUGUI Cost;
        public Image Artwork;
    }
}