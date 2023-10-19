using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
   [SerializeField] private Slider volumeSlider;

    private void Start() {
        if (!PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetFloat("soundVolume", 1);
            Load();
        }   
    }
   public void ChangeVolume(){
        AudioListener.volume = volumeSlider.value;
        Save();
   }

    public void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }

    public void Save(){
        PlayerPrefs.SetFloat("soundVolume", volumeSlider.value);
   
    }
}


