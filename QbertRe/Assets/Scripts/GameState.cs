using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    private int cubesValidated = 0;
    private int numberOfCubes = 28;


    //Used by Cube when player hits enough a cube, or during the flip color level
    public void addCube()
    {
        cubesValidated++;

        if(cubesValidated == numberOfCubes) //WIN GG MATE
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().loadNextLevel();

            int lives = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().getLives();
            GameObject.Find("GameManager").GetComponent<GameManager>().setLives(lives);
        }
    }


    //Used by Cube when green creature hits, or during the flip color level
    public void removeCube()
    {
        cubesValidated--;
    }
}
