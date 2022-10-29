using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDest : MonoBehaviour
{
    public int life;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        life--;
        if (life<1) { Destroy(gameObject); }
        //this script destroys a gameobject after a certain amount of time (int life)
    }
}
