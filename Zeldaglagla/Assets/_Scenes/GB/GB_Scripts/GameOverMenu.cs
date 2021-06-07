using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public void LoadCheckpoint()
    {

        Debug.Log("Checkpoint Loading Ok !");
        //SceneManager.LoadScene("MettreCheckpoint");
    }
    
    public void MainMenu()
    {
        Debug.Log("Menu Loading Ok !");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game Ok !"); 
        Application.Quit();
    }

}
