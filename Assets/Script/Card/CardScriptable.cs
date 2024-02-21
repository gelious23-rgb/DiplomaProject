using UnityEngine;

namespace Script.Card
{
    
    public class CardScriptable : ScriptableObject
    {
        private string Name;
        public int Hp;
        public int Attack;
        public int Manacost;

        
        [TextArea]  
        public string Description;
        public  Types CardType;
        public Sprite CardImage;
        
        [System.Serializable]
        public enum Types
        {
            Tool, Man, Powers, Relic, Heroic
        };



    }

}
