using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectGround : MonoBehaviour
{
    public player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        player.jump = 2;
    }
}
