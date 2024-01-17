using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSettings : MonoBehaviour
{
    [SerializeField] private Toggle PublicToggle;
    [SerializeField] private Toggle RandomFactions;

    public void Start()
    {
        PublicToggle.isOn = false;
        RandomFactions.isOn = false;
    }

}