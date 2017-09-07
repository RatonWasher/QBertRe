using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{

    public enum ballTypeEnum { Red, Green };


    private float height = 2.5f;
    private float incrementor = 0;
    private bool isJumping = false;

    private ballTypeEnum ballType;
    private float freezeTime = 4f;
    private int greenBallScore = 100;
    
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

        //Touching the ground
        transform.triggerNewJump(ref col, ref startPos, ref endPos, ref isJumping, ref incrementor);

        //Falling off the stage
        if (col.gameObject.tag == "WorldBound")
        {
            Destroy(gameObject);
        }

        //Colliding with the player
        if (col.gameObject.tag == "Player")
        {
            if (ballType == ballTypeEnum.Red)
            {
                col.gameObject.GetComponent<PlayerController>().Death();
            }
            else if (ballType == ballTypeEnum.Green)
            {
                Destroy(gameObject);
                col.gameObject.GetComponent<PlayerController>().freezeAllEnnemies(freezeTime);
                GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<UIManager>().addScore(greenBallScore);
            }
        }
    }


    public void setBallType(ballTypeEnum newBallType)
    {
        ballType = newBallType;
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
