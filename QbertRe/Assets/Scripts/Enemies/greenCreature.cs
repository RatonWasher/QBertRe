using UnityEngine;
using System.Collections;

public class greenCreature : MonoBehaviour {

    private float height = 2.5f;
    private float incrementor = 0;
    private bool isJumping = false;

    private Vector3 startPos;
    private Vector3 endPos;
    private int greenCreatureScore = 300;
    private bool freeze = false;
	
	

	void Update ()
    {
        if(!freeze)
            transform.Jump(ref startPos, ref endPos, ref isJumping, ref incrementor, ref height);
    }


    void OnCollisionEnter(Collision col)
    {
        //Movement
        transform.triggerNewJump(ref col, ref startPos, ref endPos, ref isJumping, ref incrementor);

        //Falling off the level
        if (col.gameObject.tag == "WorldBound")
        {
            Destroy(gameObject);
        }

        //Collision with player
        if (col.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<UIManager>().addScore(greenCreatureScore);
            Destroy(gameObject);
        }
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
