using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas = null;
    [SerializeField] private GameObject endCanvas = null;
    [SerializeField] private GameObject aimCanvas = null;
    [SerializeField] private GameObject thirdCanvas = null;


    private void Update() {
        if(Input.GetKey(KeyCode.Escape)){
            Pause();

        }
    }

    public void Pause(){
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
        thirdCanvas.SetActive(false);
        aimCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume(){
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        thirdCanvas.SetActive(true);
        aimCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
    
}
