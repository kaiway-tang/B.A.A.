using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class americaProj : MonoBehaviour
{
    public int function; //0:cannon bullet 1:air strike up 2: air strike down 3: fissure summon 4: fissure bullet
    public GameObject bullet;
    int del;
    // Start is called before the first frame update
    void Start()
    {
        if (function == 0||function==2)
        {
            transform.Rotate(Vector3.forward * 180);
            //if the bullet is a cannon bullet or the second phase of the air strike attack it rotates 180
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (function < 3)
        {
            transform.position += transform.up * .6f; //moves cannon or airstrike bullets forward quickly
        }
        if (function==3)
        {
            if (del<1) { del = 8; Instantiate(bullet, new Vector3(transform.position.x, -1.5f, 0), transform.rotation); }
            del--;
            transform.position += transform.right * .06f;
            //moves the fissure attack forward slowly
        }
        if (function==4)
        {
            transform.position += transform.up * .23f; //moves the fissure bullets upwards
        }
    }
}
