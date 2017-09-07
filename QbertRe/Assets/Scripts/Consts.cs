using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Consts : MonoBehaviour {

    public static List<Color> Colors = new List<Color>();



    void Awake()
    {
        Colors.Add(new Color32(0, 255, 0, 255)); //Green Light
        Colors.Add(new Color32(255, 255, 0, 255)); //Yellow
        Colors.Add(new Color32(237, 28, 36, 255)); //Red
        Colors.Add(new Color32(0, 255, 255, 255)); //Cyan
        Colors.Add(new Color32(255, 127, 39, 255)); //Orange
        Colors.Add(new Color32(63, 72, 204, 255)); //Dark blue
    }
   
}
