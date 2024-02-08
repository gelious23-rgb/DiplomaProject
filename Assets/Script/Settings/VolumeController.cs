using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public TextMeshProUGUI volumeText;
    public AudioSource music;
    public Slider volumeSlider;
    private bool isMuted;
    [SerializeField] private Image muteButton;
    [SerializeField] private Sprite unmuted;
    [SerializeField] private Sprite muted;


    private float musicVolume = 1f;

    void Start()
    {
        UpdateVolumeText();

        music = GetComponent<AudioSource>();
        volumeSlider.value= musicVolume;
        isMuted = PlayerPrefs.GetInt("MUTED") == 1;
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

    public void SetVolume(float volume)
    { 
        musicVolume = volume;
    }
    public void OnSliderValueChanged()
    {
        musicVolume = volumeSlider.value;
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
