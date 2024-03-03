using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonSounds : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        audioManager.Instance.playSound(audioStorage.audioType.buttonClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioManager.Instance.playSound(audioStorage.audioType.buttonHover);
    }
}
