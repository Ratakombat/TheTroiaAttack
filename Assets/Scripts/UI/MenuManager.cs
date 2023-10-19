using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas = null;
    [SerializeField] private GameObject optionsCanvas = null;


    private void Start() {
        ActivateMainMenu(true);
    }


    public void ActivateMainMenu(bool state){
        mainMenuCanvas.SetActive(state);
        optionsCanvas.SetActive(!state);

    }

    public void Play(){
        SceneManager.LoadScene(1);

    }

    public void ParkourMode(){
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Quitt(){
        Application.Quit();

    }

    public void LowQuality(){
        QualitySettings.SetQualityLevel(0, true);
    }

    public void MediumQuality(){
        QualitySettings.SetQualityLevel(1, true);
    }

    public void HighQuality(){
        QualitySettings.SetQualityLevel(2, true);
    }

    public void Sensitivity(){
        
    }
}
