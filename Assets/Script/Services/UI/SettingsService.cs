using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script.Services.UI
{
    public class SettingsService : MonoBehaviour
    {
        private const string Hz = " Hz";
        private TMP_Dropdown _resolutionDropdown;
        private Resolution[] _resolutions;
        private List<Resolution> _resolutionList;
        private float _currentRefreshrate;
        private int _resolutionIndex = 0;
    

        private void Start()
        {
            RefreshResolution();

            var options = CreateResolutions();
            _resolutionDropdown.AddOptions(options);
            _resolutionDropdown.value = _resolutionIndex; 
            _resolutionDropdown.RefreshShownValue();
        }

        public void FullScreen(bool fsc)
        {
            Screen.fullScreen = fsc;
            Debug.Log("Fullscreen is " + fsc);
        }

        public void SetScreenRes(int resIndex)
        {
            Resolution resolution = _resolutionList[_resolutionIndex]; 
            Screen.SetResolution(resolution.width, resolution.height, true);
        
        }

        private void RefreshResolution()
        {
            _resolutions = Screen.resolutions;
            _resolutionList = new List<Resolution>();
            _resolutionDropdown.ClearOptions();
            _currentRefreshrate = Screen.currentResolution.refreshRate;
            for (int i = 0; i < _resolutions.Length; i++)
            {
                if (_resolutions[i].refreshRate == _currentRefreshrate)
                {
                    _resolutionList.Add(_resolutions[i]);
                }
            }
        }

        private List<string> CreateResolutions()
        {
            List<string> options = new List<string>();
            for (int i = 0; i < _resolutionList.Count; i++)
            {
                string resolutionOption = _resolutionList[i].width + "x" + _resolutionList[i].height + " " +
                                          _resolutionList[i].refreshRate + Hz; 
                options.Add(resolutionOption);
                
                if (_resolutionList[i].width == Screen.width && _resolutionList[i].height == Screen.height)
                    _resolutionIndex = i;
            }

            return options;
        }
    }
}
