using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class PlayerController : MonoBehaviour {
    
    public int lives;
    public bool cheatCode = false;

    private float height = 2f;
    private float incrementor = 0;
    private bool isOnTheGround = false;
    private bool controlsEnabled = false;

    private List<Vector3> disksPos = new List<Vector3>();
    private Vector3 spawnPos;
    private Vector3 startPos;
    private Vector3 endPos;

    private Rigidbody RB;
    private int distanceKillSnake = 4;


    void Start ()
    {
        spawnPos = transform.position;
        RB = GetComponent<Rigidbody>();

        //Finding disks position
        GameObject[] disks = GameObject.FindGameObjectsWithTag("Disk");

        foreach (GameObject disk in disks)
        {
            disksPos.Add(disk.transform.position);
            Debug.Log(disk.transform.position);
        }
        
    }
	

	void Update ()
    {
        if(cheatCode)
        {
            if (Input.GetKeyDown(KeyCode.T))
                freezeAllEnnemies(10f);
        }

        if (controlsEnabled)
        {
            //Takes the player input to prepare the jump
            if (isOnTheGround)
            {
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown("up"))
                    setUpJump(-1, 1, 0);

                if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown("left"))
                    setUpJump(0, 1, -1);

                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown("down"))
                    setUpJump(1, -1, 0);

                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown("right"))
                    setUpJump(0, -1, 1);
            }

            //Calculates and makes the jump after an input
            if (!isOnTheGround)
            {
                incrementor += 0.04f;
                Vector3 currentPos = Vector3.Lerp(startPos, endPos, incrementor);
                currentPos.y += height * Mathf.Sin(Mathf.Clamp01(incrementor) * Mathf.PI);
                transform.position = currentPos;
            }

            //Player just touched the ground
            if (!isOnTheGround && transform.position == endPos)
            {
                isOnTheGround = true;
                incrementor = 0;

                //Update start and end position for the snake chase
                Vector3 tempPos = startPos;
                startPos = transform.position;
                endPos = tempPos;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "SpawnPoint") {
            isOnTheGround = true;
            enableControls();
        }

        if (col.gameObject.tag == "WorldBound")
        {
            Death();
        }

        if (col.gameObject.tag == "Disk")
        {
            disableControls();
            isOnTheGround = false;

            returnToSpawn();
            killAllEnnemies();
            resetAllSpawns();
            
            Destroy(col.gameObject); //Destroy touched disk
        }

    }


    //Placing right the player if he'll jump on a disk
    private Vector3 checkDisk(Vector3 pos)
    {
        if (disksPos.Contains(pos))
        {
            pos.y -= 1.3f;
            Debug.Log("hey");
        }
        
        return pos;
    }

    //Defining positions before jumping while input
    private void setUpJump(int _x, int _y, int _z)
    {
        startPos = transform.position;
        endPos = transform.position = new Vector3(transform.position.x + _x,
                                        transform.position.y + _y,
                                        transform.position.z + _z); ;
        endPos = checkDisk(endPos);


        isOnTheGround = false;
    }


    //Player death
    public void Death()
    {
        returnToSpawn();
        killAllEnnemies();
        resetAllSpawns();

        actualizeLife(-1);
        if (lives <= 0)
            SceneManager.LoadScene("Menu");
    }


    //Modify life and update UI
    private void actualizeLife(int lifeChange)
    {
        lives += lifeChange;
        GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<UIManager>().updateLivesUI(lives);
    }


    //Return the player to the spawn
    private void returnToSpawn()
    {
        incrementor = 0;
        isOnTheGround = false;

        resetSpawnBlock();
        transform.position = spawnPos;
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
    }


    //Destroy all ennemies tag
    private void killAllEnnemies(bool isDisk = false)
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            //The snake needs to be near to get killed by disk
            if (enemy.name == "Snake(Clone)" && isDisk)
            {
                continue;
            }
            Destroy(enemy);
        }
    }


    //Used by Ball to get all enemies and then call the freeze method
    public void freezeAllEnnemies(float freezeTime)
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Rigidbody>().isKinematic = true;
            if (enemy.GetComponent<Ball>() != null)
                enemy.GetComponent<Ball>().setFreeze();
            if (enemy.GetComponent<Snake>() != null)
                enemy.GetComponent<Snake>().setFreeze();
            if (enemy.GetComponent<SnakeEgg>() != null)
                enemy.GetComponent<SnakeEgg>().setFreeze();
            if (enemy.GetComponent<purpleCreature>() != null)
                enemy.GetComponent<purpleCreature>().setFreeze();
            if (enemy.GetComponent<greenCreature>() != null)
                enemy.GetComponent<greenCreature>().setFreeze();
        }

        GameObject.Find("EnemyGenerator").GetComponent<SpawnBalls>().pauseSpawn();
        GameObject.Find("EnemyGenerator").GetComponent<SpawnPurpleCreature>().pauseSpawn();
        GameObject.Find("EnemyGenerator").GetComponent<SpawnGreenCreature>().pauseSpawn();
        GameObject.Find("EnemyGenerator").GetComponent<SpawnSnakeEgg>().pauseSpawn();

        StartCoroutine(Unfreeze(freezeTime));
    }


    //Used to freeze all ennemies when touching a green ball
    private IEnumerator Unfreeze(float freezeTime)
    {
        yield return new WaitForSeconds(freezeTime);

        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Rigidbody>().isKinematic = false;
            if (enemy.GetComponent<Ball>() != null)
                enemy.GetComponent<Ball>().unsetFreeze();
            if (enemy.GetComponent<Snake>() != null)
                enemy.GetComponent<Snake>().unsetFreeze();
            if (enemy.GetComponent<SnakeEgg>() != null)
                enemy.GetComponent<SnakeEgg>().unsetFreeze();
            if (enemy.GetComponent<purpleCreature>() != null)
                enemy.GetComponent<purpleCreature>().unsetFreeze();
            if (enemy.GetComponent<greenCreature>() != null)
                enemy.GetComponent<greenCreature>().unsetFreeze();
        }

        GameObject.Find("EnemyGenerator").GetComponent<SpawnBalls>().resumeSpawn();
        GameObject.Find("EnemyGenerator").GetComponent<SpawnPurpleCreature>().resumeSpawn();
        GameObject.Find("EnemyGenerator").GetComponent<SpawnGreenCreature>().resumeSpawn();
        GameObject.Find("EnemyGenerator").GetComponent<SpawnSnakeEgg>().resumeSpawn();
    }


    //Reset all spawner's timer (the player will not be instakilled if an ennemy spawn just when the player respawns)
    private void resetAllSpawns()
    {
        GameObject.Find("EnemyGenerator").GetComponent<SpawnGreenCreature>().resetRespawn();
        GameObject.Find("EnemyGenerator").GetComponent<SpawnPurpleCreature>().resetRespawn();
        GameObject.Find("EnemyGenerator").GetComponent<SpawnBalls>().resetRespawn();
    }


    //Used to reset the spawn block (return to spawn is not considered as a step on the cube)
    private void resetSpawnBlock()
    {
        GameObject.FindGameObjectWithTag("SpawnPoint").GetComponent<Cube>().spawnBlockProtect();
    }


    //Used by UIManager to add a life when score += 8000
    public void addLife()
    {
        actualizeLife(1);
    }


    //Used by EnemiesMouvement for the snake chase
    public Vector3 getStartPos()
    {
        return startPos;
    }


    //Used by GameManager to set lives at the beginning of a level
    public void setLives(int _lives)
    {
        lives = _lives;
        actualizeLife(0);
    }


    //Used by GameState at level end to save the life between levels
    public int getLives()
    {
        return lives;
    }

    public void enableControls()
    {
        controlsEnabled = true;
    }

    public void disableControls()
    {
        controlsEnabled = false;
    }

}
