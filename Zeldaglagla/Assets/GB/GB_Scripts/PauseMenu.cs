using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField] GameObject pauseMenu;
    public GameObject optionsMenu;

    private void Awake()
    {
        AudioManager.volumeSlider = 1f;
    }
    
    void Update()
    {
        if (Gamepad.current.startButton.wasPressedThisFrame || Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        Time.timeScale = 0f;
        isGamePaused = true;
        Debug.Log("Pause Ok");
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        Debug.Log("Resume Ok");
    }

    public void OptionsMenu()
    {
        //SceneManager.LoadScene("OptionsMenu");
        Debug.Log("Options Menu Ok");
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    

    public void LoadMenu()
    {
        //pour reload la scène actuelle
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        SceneManager.LoadScene("MainMenu");
        Debug.Log("Load Menu Ok");
        Time.timeScale = 1f;
    }

    public void GameOverMenu()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Ok");
    }



}
