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
    }

    void Update()
    {
        ESCOpen();
    }

    public void Option()
    {
        uiOption.SetActive(true);
    }

    public void Back()
    {
        Time.timeScale = 1f;
        uiOption.SetActive(false);
    }

    public void ChangeMusicVolume()
    {
        float value = MusicVol.value;

        float dB = Mathf.Log10(Mathf.Max(0.0001f, value)) * 20;
        mainAuio.SetFloat("MusicVol", dB);

        PlayerPrefs.SetFloat("MusicVol", value);
    }
    public void ChangeSFXVolume()
    {
        float value = SfxVol.value;

        float dB = Mathf.Log10(Mathf.Max(0.0001f, value)) * 20;
        mainAuio.SetFloat("SfxVol", dB);

        PlayerPrefs.SetFloat("SfxVol", value);
    }

    public void ESCOpen()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Time.timeScale = 0f;
            uiESC.SetActive(true);
        }

    }

    public void ESCBack()
    {
        Time.timeScale = 1f;
        uiESC.SetActive(false);
        
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
