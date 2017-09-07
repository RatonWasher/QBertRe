using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class EnemiesMouvements {



    public static void triggerNewJump(this Transform transform, ref Collision col, ref Vector3 startPos, ref Vector3 endPos, ref bool isJumping, ref float incrementor)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "SpawnPoint")
        {
            transform.Land(ref startPos, ref endPos, ref isJumping, ref incrementor);
            float rand = UnityEngine.Random.value;

            if (rand > 0.5f)
            {
                startPos = transform.position;
                endPos = new Vector3(transform.position.x + 1,
                                     transform.position.y - 1,
                                     transform.position.z);

                isJumping = true;
            }
            else
            {
                startPos = transform.position;
                endPos = new Vector3(transform.position.x,
                                     transform.position.y - 1,
                                     transform.position.z + 1);

                isJumping = true;
            }
        }
    }


    public static void Climb(this Transform transform, ref Collision col, ref Vector3 startPos, ref Vector3 endPos, ref bool isJumping, ref float incrementor, int bottomPos)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "SpawnPoint")
        {
            transform.Land(ref startPos, ref endPos, ref isJumping, ref incrementor);

            if (bottomPos > 0)
            {
                startPos = transform.position;
                endPos = new Vector3(transform.position.x - 1,
                                     transform.position.y + 1,
                                     transform.position.z);

                isJumping = true;
            }
            else
            {
                startPos = transform.position;
                endPos = new Vector3(transform.position.x,
                                     transform.position.y + 1,
                                     transform.position.z - 1);

                isJumping = true;
            }
        }
    }


    public static void chasePlayer(this Transform transform, ref Collision col, ref Vector3 startPos, ref Vector3 endPos, ref bool isJumping, ref float incrementor, List<Vector3> cubesPos)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "SpawnPoint")
        {
            transform.Land(ref startPos, ref endPos, ref isJumping, ref incrementor);
            startPos = transform.position;


            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().getStartPos();
            Vector3 distance = playerPos - transform.position;


            //If the player is far to the x
            if (Math.Abs(distance.x) >= Math.Abs(distance.z))
            {
                if (distance.x > 0)
                {
                    endPos = transform.createEndPos(1f, -1f, 0f); //Gain 1x (down)

                    if (cubesPos.Contains(endPos)) //If there is a cube where the snake is going to jump
                    {
                        endPos = transform.createEndPos(0f, 1f, -1f); //Lose 1z (up)
                    }
                }
                else
                {
                    endPos = transform.createEndPos(-1f, 1f, 0f); //Lose 1x (up)
                }
            }
            //If the player is far to the z
            else
            {
                if (distance.z > 0)
                {
                    endPos = transform.createEndPos(0f, -1f, 1f); //Gain 1z (down)

                    if (cubesPos.Contains(endPos)) //If there is a cube where the snake is going to jump
                    {
                        endPos = transform.createEndPos(1f, -1f, 0f); //Gain 1x (down)
                    }
                }
                else
                {
                    endPos = transform.createEndPos(0f, 1f, -1f); //Lose 1z (up)
                }
            }

            isJumping = true;
        }
    }


    public static void Land(this Transform transform, ref Vector3 startPos, ref Vector3 endPos, ref bool isJumping, ref float incrementor)
    {
        isJumping = false;
        incrementor = 0;
        Vector3 tempPos = startPos;
        startPos = transform.position;
        endPos = tempPos;
    }


    public static void Jump(this Transform transform, ref Vector3 startPos, ref Vector3 endPos, ref bool isJumping, ref float incrementor, ref float height)
    {
        if (isJumping)
        {
            incrementor += 0.02f;
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, incrementor);
            currentPos.y += height * Mathf.Sin(Mathf.Clamp01(incrementor) * Mathf.PI);
            transform.position = currentPos;
        }
        if (transform.position == endPos)
        {
            transform.Land(ref startPos, ref endPos, ref isJumping, ref incrementor);
        }
    }


    public static Vector3 createEndPos(this Transform transform, float _x, float _y, float _z)
    {
        Vector3 endPos = new Vector3(transform.position.x + _x,
                                     transform.position.y + _y,
                                     transform.position.z + _z);
        return endPos;
    }
}
