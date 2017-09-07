using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {



    public void playGame()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }


    public void quitGame()
    {
        Application.Quit();
    }
}
