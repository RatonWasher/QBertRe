using UnityEngine;
using System.Collections;

public class SpawnSnakeEgg : MonoBehaviour {

    public GameObject SnakeEgg;

    public Vector2 spawnRate = new Vector2(2.5f, 4f);
    private Vector3 spawnPos = new Vector3(0.1f, 11f, 0.1f);
    private bool isPaused = false;



    void Start()
    {
        StartCoroutine(spawnSnake());
    }


    IEnumerator spawnSnake()
    {
        while (isPaused)
        {
            yield return null;
        }

        //Determine a random new interval before the next snake egg's spawning
        float randTimer = Random.Range(spawnRate[0], spawnRate[1]);
        yield return new WaitForSeconds(randTimer);

        //Instanciate the new snake egg
        GameObject newSnakeEgg =
            (GameObject)Instantiate(SnakeEgg, spawnPos, Quaternion.identity);
    }

    
    //Used by Player to reset respawn time if the players die (to avoid instakill)
    public void resetRespawn()
    {
        StopAllCoroutines();
        StartCoroutine(spawnSnake());
    }


    //Used by Snake to make a new snake egg spawn when it dies
    public void snakeDeath()
    {
        StartCoroutine(spawnSnake());
    }

    public void pauseSpawn()
    {
        isPaused = true;
    }

    public void resumeSpawn()
    {
        isPaused = false;
    }
}
