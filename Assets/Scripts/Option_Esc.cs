using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option_Esc : MonoBehaviour
{
    public GameObject uiOption;
    public GameObject uiESC;

    public Slider MusicVol;
    public Slider SfxVol;
    public AudioMixer mainAuio;

    public Button backButton;
    public Button menuButton;
    public Button HistoryButton;

    public Toggle musicToggle;
    public Toggle sfxToggle;

    float lastMusicVolume;
    float lastSFXVolume;

    public TextMeshProUGUI musicPercentText;
    public TextMeshProUGUI sfxPercentText;

    public AudioClip click;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        backButton.onClick.AddListener(() =>
        {
            audioSource.PlayOneShot(click);
            Time.timeScale = 1f;
            uiESC.SetActive(false);
        });

        if (menuButton != null)
            menuButton.onClick.AddListener(() => StartCoroutine(PlaySoundAndLoad()));

        if (HistoryButton != null)
            HistoryButton.onClick.AddListener(() => audioSource.PlayOneShot(click));

    }

    IEnumerator PlaySoundAndLoad()
    {
        if (audioSource != null && click != null)
        {
            audioSource.PlayOneShot(click);
        }

        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(0.25f);

        SceneManager.LoadScene("Main Menu");
    }


    void Start()
    {
        float music = PlayerPrefs.GetFloat("MusicVol", 1f);
        float sfx = PlayerPrefs.GetFloat("SfxVol", 1f);

        MusicVol.value = music;
        SfxVol.value = sfx;

        float musicdB = Mathf.Log10(Mathf.Max(0.0001f, music)) * 20;
        float sfxdB = Mathf.Log10(Mathf.Max(0.0001f, sfx)) * 20;

        mainAuio.SetFloat("MusicVol", musicdB);
        mainAuio.SetFloat("SfxVol", sfxdB);

        musicPercentText.text = Mathf.RoundToInt(music * 100f) + "%";
        sfxPercentText.text = Mathf.RoundToInt(sfx * 100f) + "%";
    }

    void Update()
    {
        ESCOpen();
    }

    public void Option()
    {
        audioSource.PlayOneShot(click);
        uiOption.SetActive(true);
    }

    public void ChangeMusicVolume()
    {
        float value = MusicVol.value;

        float dB = Mathf.Log10(Mathf.Max(0.0001f, value)) * 20;
        mainAuio.SetFloat("MusicVol", dB);

        PlayerPrefs.SetFloat("MusicVol", value);
        musicPercentText.text = Mathf.RoundToInt(value * 100f) + "%";
    }
    public void ChangeSFXVolume()
    {
        float value = SfxVol.value;

        float dB = Mathf.Log10(Mathf.Max(0.0001f, value)) * 20;
        mainAuio.SetFloat("SfxVol", dB);

        PlayerPrefs.SetFloat("SfxVol", value);
        sfxPercentText.text = Mathf.RoundToInt(value * 100f) + "%";
    }

    public void ESCOpen()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Time.timeScale = 0f;
            uiESC.SetActive(true);
        }
    }

    public void ToggleMusic()
    {
        if (musicToggle.isOn)
        {
            lastMusicVolume = MusicVol.value;
            musicPercentText.text = "0%";

            MusicVol.SetValueWithoutNotify(0);
            mainAuio.SetFloat("MusicVol", -80f);
        }
        else
        {
            MusicVol.SetValueWithoutNotify(lastMusicVolume);

            ChangeMusicVolume();
        }
    }

    public void Togglesfx()
    {
        if (sfxToggle.isOn)
        {
            lastSFXVolume = SfxVol.value;
            sfxPercentText.text = "0%";

            SfxVol.SetValueWithoutNotify(0);
            mainAuio.SetFloat("SfxVol", -80f);
        }
        else
        {
            SfxVol.SetValueWithoutNotify(lastSFXVolume);

            ChangeSFXVolume();
        }
    }
}
