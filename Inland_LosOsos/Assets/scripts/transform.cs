using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour
{
    public Transform point;
    bool every2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (every2)
        {
            transform.position = point.position;
            every2 = false;
        } else
        {
            every2 = true;
        }
    }
}
