using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenModeSettings : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown ScreenModeDropDown;
    private Resolutions resolutions;
    void Start()
    {
        resolutions = FindObjectOfType<Resolutions>();
        int val = PlayerPrefs.GetInt("ScreenMode");
        ScreenModeDropDown.value = val;
    }

    public void SetScreenMode(int index)
    {
        PlayerPrefs.SetInt("ScreenMode", index);
        if (index == 0)
        {

            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        if (index == 1)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        if (index == 2)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
