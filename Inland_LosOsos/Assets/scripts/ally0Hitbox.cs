using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ally0Hitbox : MonoBehaviour
{
    public GameObject tanky;
    public ally0 ally0;
    public int del;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        if (del==350) { tanky.transform.localScale = new Vector3(.28f, .28f, 1); }
        if (del>0) { del--; }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if ally0 detects an enemy projectile above it will use its special ability, super shield, to block it
        if (col.gameObject.tag == "nmyAtk"&&del<1)
        {
            tanky.transform.localScale = new Vector3(.6f, .6f, 1);
            del = 450;
        }
    }
}
