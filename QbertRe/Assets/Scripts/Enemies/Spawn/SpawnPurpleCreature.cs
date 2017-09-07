using UnityEngine;
using System.Collections;

public class SpawnPurpleCreature : MonoBehaviour {

    public GameObject purpleCreature;
    public Vector2 spawnRate = new Vector2(10f, 15f);

    private Vector3[] spawnPos = { new Vector3(0.1f, 20f, 6.1f), new Vector3(6.1f, 20f, 0.1f) };
    private bool isPaused = false;



    void Start() {
        StartCoroutine(spawnPurpleCreature());
    }


    IEnumerator spawnPurpleCreature()
    {
        //while (true)
        //{
            while (isPaused)
            {
                yield return null;
            }

            //Determine a random new interval before the next creature's spawning
            float randTimer = Random.Range(spawnRate[0], spawnRate[1]);
            yield return new WaitForSeconds(randTimer);


            //Determine random value (0 or 1) for purple creature position (right or left)
            int randPos = (int)Mathf.Round(Random.value);


            //Instanciate the new purple creature
            GameObject newPurpleCreature =
                (GameObject)Instantiate(purpleCreature, spawnPos[randPos], Quaternion.identity);
            newPurpleCreature.GetComponent<purpleCreature>().setBottomPos(randPos);
            
        //}
    }

    public void resetRespawn()
    {
        StopAllCoroutines();
        StartCoroutine(spawnPurpleCreature());
    }

    public void pauseSpawn()
    {
        isPaused = true;
        Debug.Log("hey");
    }

    public void resumeSpawn()
    {
        isPaused = false;
    }
}
