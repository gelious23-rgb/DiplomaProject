using System;
 
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class HideScript : NetworkBehaviour
    {
        public Image thisImage;
        public Sprite HellCardSp, HeavenCardSp;

        private void Start()
        {
            if (!IsHost)
            {
                thisImage.sprite = HellCardSp;
                thisImage.gameObject.SetActive(true);
            }
            else
            {
                thisImage.sprite = HeavenCardSp;
                
            }
        }
    }
}
