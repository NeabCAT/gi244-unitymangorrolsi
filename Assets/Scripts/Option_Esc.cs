using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option_Esc : MonoBehaviour
{
    public GameObject uiOption;
    public GameObject uiESC;

    public Slider Music_Vol;
    public Slider SFX_Vol;
    public AudioMixer mainAuio;

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
        float value = Music_Vol.value;

        if (value <= 0.001f)
        {
            mainAuio.SetFloat("MusicVol", -80f); // mute จริง
            return;
        }

        float dB = Mathf.Log10(value) * 20;
        mainAuio.SetFloat("MusicVol", dB);
    }
    public void ChangeSFXVolume()
    {
        float dB = Mathf.Log10(Mathf.Max(0.0001f, SFX_Vol.value)) * 20;
        mainAuio.SetFloat("SfxVol", dB);
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
        SceneManager.LoadScene("MainMenu");
    }
}
