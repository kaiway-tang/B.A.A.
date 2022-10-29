using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = manager.camTrans;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
