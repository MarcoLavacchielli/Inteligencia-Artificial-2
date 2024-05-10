using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("ValorSliderMusica"))
        {
            float valorRecuperadoMusica = PlayerPrefs.GetFloat("ValorSliderMusica");
            musicSlider.value = valorRecuperadoMusica;
        }

        if (PlayerPrefs.HasKey("ValorSliderSFX"))
        {
            float valorRecuperadoSFX = PlayerPrefs.GetFloat("ValorSliderSFX");
            sfxSlider.value = valorRecuperadoSFX;
        }
    }

    public void PlayFirstMusic()
    {
        AudioManager.Instance.PlayFirstMusic();
    }

    public void AdjustMusicVolume(float volume)
    {
        var musicSliders = FindObjectsOfType<Slider>().OfType<Slider>().Where(slider => slider.name == "MusicSlider").ToList();
        musicSliders.ForEach(slider => slider.value = volume);
        AudioManager.Instance.MusicVolume(volume);
    }

    public void AdjustSFXVolume(float volume)
    {
        var sfxSliders = FindObjectsOfType<Slider>().OfType<Slider>().Where(slider => slider.name == "SFXSlider").ToList();
        sfxSliders.ForEach(slider => slider.value = volume);
        AudioManager.Instance.SFXVolume(volume);
    }
}
