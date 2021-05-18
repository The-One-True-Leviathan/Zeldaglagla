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
    }/*public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("GB_TestScene");
        Debug.Log("Play Game OK");
    }

    public void OptionsMenu()
    {
        Debug.Log("Options Menu OK");
    }

    public void Credits()
    {
        Debug.Log("Credits OK");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game OK");
        Application.Quit();
    }*/

}