using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject hudCanvas = null;
    [SerializeField] private GameObject endCanvas = null;

    private void Start() {
        SetActiveHud(true);
    }
    public void SetActiveHud(bool state){
   
            hudCanvas.SetActive(state);
            endCanvas.SetActive(!state);
    }

    public void Restart(){
        SceneManager.LoadScene(1);
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;


    }

    public void Quit(){
        Application.Quit();
    }

   
}