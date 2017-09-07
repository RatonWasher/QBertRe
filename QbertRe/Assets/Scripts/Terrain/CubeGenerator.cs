using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CubeGenerator : MonoBehaviour {

    public int size = 7;
    public GameObject cubePrefab;
    int level;

    private Color color0;
    private Color color1;
    private Color color2;
    private List<Color> ColorsGenerated = new List<Color>();
    
    private Cube[] cubes = new Cube[7];
    private List<Vector3> cubesPos = new List<Vector3>();



    void Awake()
    {
        var rng = new System.Random();
        var values = Enumerable.Range(0, Consts.Colors.Count).OrderBy(x => rng.Next()).ToArray();

        ColorsGenerated.Add(Consts.Colors[values[0]]);
        ColorsGenerated.Add(Consts.Colors[values[1]]);
        ColorsGenerated.Add(Consts.Colors[values[2]]);
    }


    public void Generate()
    {
        int newSize = size;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < newSize; j++)
            {
                //Adding the futur cube's position to the list
                Vector3 pos = new Vector3(i, size - (i + j), j);
                cubesPos.Add(pos);

                Cube newCube = new Cube(pos);
                cubes[i] = newCube;
                GameObject cubeObject = (GameObject)Instantiate(cubePrefab, newCube.getPosition(), Quaternion.identity);
                
                cubeObject.GetComponent<Cube>().setLevel(level);
                cubeObject.GetComponent<Cube>().Instantiate();
                
                if (i == 0 && j == 0) //SpawnBlock
                {
                    cubeObject.tag = "SpawnPoint";
                    cubeObject.GetComponent<Cube>().spawnBlockProtect();
                }
            }

            newSize--;
        }
    }


    //Used by GameManager to define the level before the cubes generation
    public void setLevel(int _level)
    {
        level = _level;
    }


    //Used by Snake to know if there is a cube where he will jump
    public List<Vector3> getCubesPos()
    {
        return cubesPos;
    }


    //Used by Cube to get generated colors
    public List<Color> getColors()
    {
        return ColorsGenerated;
    }
}