using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class audioStorage : ScriptableObject
{
    public List<audioElement> elements;
    
    [Serializable]
    public enum audioType
    {
        buttonClick,
        buttonHover
    }
    [Serializable]
    public class audioElement
    {
        public audioType type;
        public AudioClip clip;
    }

    public AudioClip Get(audioType audioType)
    {
        return elements.Find(element => element.type == audioType).clip;
    }
}
