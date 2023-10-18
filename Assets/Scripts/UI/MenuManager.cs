using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    }

    public void Quitt(){
        Application.Quit();

    }
}
