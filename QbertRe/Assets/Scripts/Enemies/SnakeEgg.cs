using UnityEngine;
using System.Collections;

public class SnakeEgg : MonoBehaviour {

    public GameObject Snake;

    private float height = 2.5f;
    private float incrementor = 0;
    private bool isJumping = false;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool freeze = false;



    void Update()
    {
        if(!freeze)
            transform.Jump(ref startPos, ref endPos, ref isJumping, ref incrementor, ref height);
    }
    

    void OnCollisionEnter(Collision col)
    {
        //Egg eclosion
        if(col.gameObject.tag == "Ground" && transform.position.y <= 2)
        {
            startPos = transform.position;
            spawnSnake();
        }

        //Movement
        transform.triggerNewJump(ref col, ref startPos, ref endPos, ref isJumping, ref incrementor);

        //Collision with player
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerController>().Death();
        }
    }


    void spawnSnake()
    {
        GameObject enemySnake =
            (GameObject)Instantiate(Snake, startPos, Quaternion.identity);
        gameObject.SetActive(false);
    }


    //Used to make a new snake egg spawn
    void OnDestroy()
    {
        GameObject.Find("EnemyGenerator").GetComponent<SpawnSnakeEgg>().snakeDeath();
    }

    public void setFreeze()
    {
        freeze = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    public void unsetFreeze()
    {
        freeze = false;
        GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionX | ~RigidbodyConstraints.FreezePositionY | ~RigidbodyConstraints.FreezePositionZ;
    }
}
