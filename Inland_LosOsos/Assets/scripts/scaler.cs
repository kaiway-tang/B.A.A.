using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaler : MonoBehaviour
{
    //this script makes the health bar follow its corresponding entity around w/o being affected by its x reflection
    public Transform user; //position of the entity of the health bar
    public Vector3 offset; //the offset of the health bar in relation to its entity
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (user == null) { Destroy(gameObject); }
        else
        {
            transform.position = user.position + offset;
        }
    }
}
