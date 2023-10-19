using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject hudCanvas = null;
    [SerializeField] private GameObject endCanvas = null;
    [SerializeField] private GameObject PauseCanvas = null;

    
    private void Start() {
        SetActiveHud(true);
        
    }

    private void Update() {
        
    }

    public void SetActiveHud(bool state){
   
            hudCanvas.SetActive(state);
            endCanvas.SetActive(!state);
            PauseCanvas.SetActive(false);
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
