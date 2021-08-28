using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameOptions : MonoBehaviour
{

    public AudioMixer masterMixer;

    //TODO: Debe tomarse el valor actual del save data del jugador
    public void SetMusicLevel(float sliderValue)
    {
        masterMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
    public void SetSFXLevel(float sliderValue)
    {
        masterMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
    }
}

//TODO: Deben guardarse las opciones en el save data
