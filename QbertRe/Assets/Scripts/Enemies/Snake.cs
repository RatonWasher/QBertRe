using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Snake : MonoBehaviour {

    private float height = 2.5f;
    private float incrementor = 0;
    private bool isJumping = false;

    private Vector3 startPos;
    private Vector3 endPos = Vector3.zero;
    private List<Vector3> cubesPos = new List<Vector3>();
    private int snakeScore = 500;
    private bool letFall = false;
    private bool freeze = false;


    
    void Start () {
        cubesPos = GameObject.Find("LevelGenerator").GetComponent<CubeGenerator>().getCubesPos();
    }
	
    
	void Update ()
    {
        if(!freeze)
            transform.Jump(ref startPos, ref endPos, ref isJumping, ref incrementor, ref height);
    }


    void OnCollisionEnter(Collision col)
    {
        //No movement if the snake has to go trought the level (disk trigger)
        if(letFall) {
            Physics.IgnoreCollision(col.collider, GetComponent<BoxCollider>());
        }
        else
        {
            //Movement
            transform.chasePlayer(ref col, ref startPos, ref endPos, ref isJumping, ref incrementor, cubesPos);
        }

        //Fall off the pyramid
        if (col.gameObject.tag == "WorldBound")
        {
            GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<UIManager>().addScore(snakeScore);
            Destroy(gameObject);
        }

        //Collision with player
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerController>().Death();
        }

        
    }


    //Used when collision with a disk to make the snake fall off the pyramid
    public void makeFall()
    {
        //GetComponent<BoxCollider>().isTrigger = true;
        letFall = true;
    }

    void OnDestroy()
    {
        GameObject.Find("EnemyGenerator").GetComponent<SpawnSnakeEgg>().snakeDeath();
    }

    public void setFreeze()
    {
        freeze = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    public void unsetFreeze()
    {
        freeze = false;
        GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionX | ~RigidbodyConstraints.FreezePositionY | ~RigidbodyConstraints.FreezePositionZ;
    }
}
