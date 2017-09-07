using UnityEngine;
using System.Collections;

public class DiskRotate : MonoBehaviour {

    public float rotateSpeed = 30f;



	void Update ()
    {
        transform.Rotate(new Vector3(0f, rotateSpeed, 0f) * Time.deltaTime);
	}
}
