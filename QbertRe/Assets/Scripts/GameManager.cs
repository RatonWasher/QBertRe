using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    int currentWorld;
    int currentLevel;

    int playerLives;
    int score;



	void Start ()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void OnLevelWasLoaded(int scene)
    {
        if(scene == 1) //Menu scene
        {
            currentLevel = 1;
            currentWorld = 2;
            playerLives = 3;
            score = 0;
        }

        if (scene == 2) //Level scene
        {
            GameObject.Find("LevelGenerator").GetComponent<CubeGenerator>().setLevel(currentWorld);
            GameObject.Find("LevelGenerator").GetComponent<CubeGenerator>().Generate();

            GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<UIManager>().updateScoreUI(score);
            GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<UIManager>().updateLevelUI(currentWorld, currentLevel);

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().setLives(playerLives);
        }


    }


    //Called by GameState
    public void loadNextLevel()
    {
        currentLevel++;

        if(currentLevel > 3)
        {
            currentLevel = 1;
            currentWorld++;
        }

        //End of game
        if(currentWorld == 3 && currentLevel == 4)
        {
            SceneManager.LoadScene("winingScreen", LoadSceneMode.Single);
        }

        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }


    //Set by UIManager
    public void addScore(int _score)
    {
        score += _score;
    }


    //Set by GameState
    public void setLives(int _lives)
    {
        playerLives = _lives;
    }


    //Get the score to display it by UIManager
    public int getScore()
    {
        return score;
    }

    
}
