using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Tooltip("Start Game Scene name.")]
    public string playGame;

    public GameObject mainMenu;
    
    [Tooltip("Options Scene name.")]
    public GameObject optionsMenu;

    [Tooltip("Credits Scene name.")]
    public GameObject creditsMenu;



    private void Awake()
    {
        AudioManager.volumeSlider = 1f;
    }


    private void Start()
    {
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
        FindObjectOfType<AudioManager>().StopPlaying("PlayMusic");
        FindObjectOfType<AudioManager>().StopPlaying("OptionsMusic");
        FindObjectOfType<AudioManager>().Play("MenuMusic");
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(playGame);
        FindObjectOfType<AudioManager>().StopPlaying("MenuMusic");
        FindObjectOfType<AudioManager>().StopPlaying("OptionsMusic");
        FindObjectOfType<AudioManager>().Play("PlayMusic");
    }

    public void OptionsMenu()
    { 
        optionsMenu.SetActive(true);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(false);
        FindObjectOfType<AudioManager>().StopPlaying("MenuMusic");
        FindObjectOfType<AudioManager>().StopPlaying("PlayMusic");
        FindObjectOfType<AudioManager>().Play("OptionsMusic");

    }

    public void CreditsMenu()
    {
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void _MainMenu()
    {
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void SetVolume(float volume)
    {
        AudioManager.volumeSlider = volume;
    }


    List<int> widths = new List<int>() { 568, 960, 1280, 1980 };
    List<int> heights = new List<int>() { 320, 540, 800, 1080 };

    public void SetScreensize(int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullscreen);
    }

    public void SetFullScreen(bool _fullscreen)
    {
        Screen.fullScreen = _fullscreen;
    }
}