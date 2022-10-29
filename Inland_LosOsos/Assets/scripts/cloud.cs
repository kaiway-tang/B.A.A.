using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(8, 55), Random.Range(-4f, 4f), 0);
        //starts the cloud a random distance from the camera
    }


    void FixedUpdate()
    {
        transform.position += new Vector3(-.05f,0,0); //moves the cloud slowly left
        if (transform.position.x<-14) { transform.position = new Vector3(Random.Range(12,35),Random.Range(-4f,4f),0); }
        //moves the cloud back to the left of the camera to pass by again if it exits the left side of the camera
    }
}
