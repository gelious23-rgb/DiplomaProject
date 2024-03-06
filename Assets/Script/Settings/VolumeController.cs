using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI volumeText;
    [SerializeField] private AudioSource music;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Image muteButton;
    [SerializeField] private Sprite unmuted;
    [SerializeField] private Sprite muted;

    private bool isMuted;

    private float musicVolume = 1f;

    void Start()
    {
        music = GetComponent<AudioSource>();
        UpdateVolumeText();

        volumeSlider.value= musicVolume;
        isMuted = PlayerPrefs.GetInt("MUTED") == 1;
        muteButton.sprite = isMuted ? muted : unmuted;
        AudioListener.pause = isMuted;

        if (!isMuted)
        {
            music.Play();
        }
    }

    void Update()
    {
        music.volume = musicVolume;
        UpdateVolumeText();
    }

    public void OnSliderValueChanged(float volume)
    {
        musicVolume = volume;
        volume = volumeSlider.value;
        UpdateVolumeText();
    }
    private void UpdateVolumeText()
    {
        if (music != null)
        {
            volumeText.text =(musicVolume * 100).ToString("F0") + "%";
        }
    }

    public void MutePressed()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        PlayerPrefs.SetInt("MUTED", isMuted? 1 : 0);
        muteButton.sprite = isMuted? muted: unmuted;
    }
}
