using UnityEngine;
using System.Collections;

public class SpawnGreenCreature : MonoBehaviour {

    public GameObject greenCreature;
    public Vector2 spawnRate = new Vector2(10f, 20f);

    private Vector3 spawnPos = new Vector3(0.1f, 11f, 0.1f);
    private bool isPaused = false;



    void Start () {
        StartCoroutine(spawnGreenCreature());
    }


    IEnumerator spawnGreenCreature()
    {
        while (true)
        {
            while (isPaused)
            {
                yield return null;
            }

            //Determine a random new interval before the next creature's spawning
            float randTimer = Random.Range(spawnRate[0], spawnRate[1]);
            yield return new WaitForSeconds(randTimer);


            //Instanciate the new green creature
            GameObject newGreenCreature =
                (GameObject)Instantiate(greenCreature, spawnPos, Quaternion.identity);
        }
    }


    public void resetRespawn()
    {
        StopAllCoroutines();
        StartCoroutine(spawnGreenCreature());
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
