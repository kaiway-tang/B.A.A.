using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMovement : MonoBehaviour
{
    public int function;//the way the object moves; 0:up and down
    public float spd;
    float del;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (function==0) { UpAndDown(); } else
        if (function==1) { } else
        if (function == 2) { } 
        if (del>0)
        {
            del--;
        }
    }
    void UpAndDown()
    {
        if (del>24)
        {
            transform.localPosition += new Vector3(0, spd, 0);
        } else
        {
            transform.localPosition -= new Vector3(0, spd, 0);
        }
        if (del==0) { del = 50; }
    }
}
