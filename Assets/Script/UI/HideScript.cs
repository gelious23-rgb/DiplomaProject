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

        /*private void Start()
        {
            if (this.gameObject.transform.parent.gameObject.CompareTag("Enemy Hand"))
            {
                thisImage.sprite = HellCardSp;
                thisImage.gameObject.SetActive(false);
            }
            else
            {
                thisImage.sprite = HeavenCardSp;
            }
        }*/
    }
}
