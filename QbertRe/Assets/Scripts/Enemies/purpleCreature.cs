using UnityEngine;
using System.Collections;

public class purpleCreature : MonoBehaviour
{

    private float height = 2.5f;
    private float incrementor = 0;
    private bool isJumping = false;

    private int bottomPos;
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
        //Movement
        transform.Climb(ref col, ref startPos, ref endPos, ref isJumping, ref incrementor, bottomPos);

        //Fall off the pyramid
        if (col.gameObject.tag == "Respawn")
        {
            Destroy(gameObject);
        }

        //Collision with player
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerController>().Death();
        }
    }

    //Used by SpawnPurpleCreature to set the spawning position (right or left)
    public void setBottomPos(int _bottomPos)
    {
        bottomPos = _bottomPos;
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
