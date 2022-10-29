using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFX : MonoBehaviour
{
    float del;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        del++;
        transform.localScale += new Vector3(-1f/6f,0.9f,0);
        if (del>11)
        {
            Destroy(gameObject);
        }
    }
}
