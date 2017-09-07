using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Cube : MonoBehaviour {

    private Color color0;
    private Color color1;
    private Color color2;
    private List<Color> Colors = new List<Color>();

    private int level; //1, 2 or 3
    private Vector3 position;
    private int neededStep;
    private Renderer rd;
    private bool isSpawnBlockProtected = false;
    private int cubeScore;
    


    //Determine colors and set needed steps
	public void Instantiate () {
        rd = GetComponent<Renderer>();
        cubeScore = 25;

        Colors = GameObject.Find("LevelGenerator").GetComponent<CubeGenerator>().getColors();

        color0 = Colors[0];
        color1 = Colors[1];
        color2 = Colors[2];

        GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<UIManager>().updateColorUI(color0);
        

        switch (level)
        {
            case 1:
                neededStep = 1;
                rd.material.color = color1;
                break;

            case 2:
                neededStep = 2;
                rd.material.color = color2;
                break;

            case 3:
                neededStep = 1;
                rd.material.color = color1;
                break;

            default:
                Debug.Log("Error dealing with the level, you're not supposed to see that");
                break;
        }
    }


    void OnCollisionEnter(Collision col)
    {
        //If the player hits an cube
        if (col.gameObject.tag == "Player" && !isSpawnBlockProtected) 
        {
            if (level == 1 || level == 2)
            {
                if (neededStep == 1) {
                    rd.material.color = color0;
                    GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<GameState>().addCube();
                }
                else if (neededStep == 2) {
                    rd.material.color = color1;
                }

                if (neededStep > 0)
                {
                    neededStep--;
                    GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<UIManager>().addScore(cubeScore);
                }
            }

            //Here we simply switch between two colors
            if (level == 3)
            {
                cubeSwitch();
                GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<UIManager>().addScore(cubeScore);
            }
            
                
        }

        //If the player hits the spawnPoint after a special action, no effect on the cube
        else if (col.gameObject.tag == "Player" && isSpawnBlockProtected)
        {
            isSpawnBlockProtected = false;
        }



        if (col.gameObject.name == "greenCreature(Clone)")
        {
            if (level == 1 || level == 2)
            {
                if (neededStep == 0)
                {
                    rd.material.color = color1;
                    GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<GameState>().removeCube();
                    neededStep++;
                }

                else if (neededStep == 1 && level == 2)
                {
                    rd.material.color = color2;
                    neededStep++;
                }
            }

            //Here we simply switch between two colors
            if (level == 3)
            {
                cubeSwitch();
            }
        }
    }

    //Used for cube switches on world 3
    private void cubeSwitch()
    {
        if (neededStep == 1)
        {
            rd.material.color = color0;
            GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<GameState>().addCube();
            neededStep = 0;
        }
        else
        {
            rd.material.color = color1;
            GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<GameState>().removeCube();
            neededStep = 1;
        }
    }


    //Constructor
    public Cube(Vector3 pos)
    {
        position = pos;
    }


    //Used by CubeGenerator to get the cube's position
    public Vector3 getPosition()
    {
        return position;
    }


    //Used if the player returns to the spawn after a special action, makes the next step on the spawn point ineffective
    public void spawnBlockProtect()
    {
        isSpawnBlockProtected = true;
    }


    //Set by CubeGenerator
    public void setLevel(int _level)
    {
        level = _level;
    }

}
