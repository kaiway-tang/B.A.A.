using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    public float move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(manager.move*move*2/3,0,0);
        //creates a parallax effect with the background using the distance traveled by the camera

        /* a parallaax effect is where the background contains multiple layers which are moved at 
        different rates to create the illusion of depth */
    }
}
