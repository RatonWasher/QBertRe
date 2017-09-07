using UnityEngine;
using System.Collections;

public class SpawnBalls : MonoBehaviour {

    public GameObject redBall;
    public GameObject greenBall;
    public Vector2 spawnRate = new Vector2(2.5f, 4f);

    private Vector3[] spawnPos = { new Vector3(0.1f, 11f, 1.1f), new Vector3(1.1f, 11f, 0.1f) };
    private float greenBallSpawnChance = 0.1f;
    private bool isPaused = false;
    


    void Start () {
        StartCoroutine(spawnBall());
    }


    IEnumerator spawnBall()
    {
        //while (true)
        //{
            while (isPaused)
            {
                yield return null;
            }

            //Determine a random new interval before the next ball's spawning
            float randTimer = Random.Range(spawnRate[0], spawnRate[1]);
            yield return new WaitForSeconds(randTimer);


            //Determine random values for ball type and position
            int randPos = (int)Mathf.Round(Random.value);
            float randBall = Random.value;
            

            //Instanciate the new ball
            GameObject newBall =
                (GameObject)Instantiate((randBall >= greenBallSpawnChance ? redBall : greenBall), spawnPos[randPos], Quaternion.identity);


            //Set the new ball's type (enum)
            Ball.ballTypeEnum ballType =
                (randBall >= greenBallSpawnChance ? Ball.ballTypeEnum.Red : Ball.ballTypeEnum.Green);
            newBall.GetComponent<Ball>().setBallType(ballType);
        //}
    }

    public void resetRespawn()
    {
        StopAllCoroutines();
        StartCoroutine(spawnBall());
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
