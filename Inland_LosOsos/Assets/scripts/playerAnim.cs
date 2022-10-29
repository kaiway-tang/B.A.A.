using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnim : MonoBehaviour
{
    public Sprite[] bscAtk;

    SpriteRenderer sprRend;
    // Start is called before the first frame update
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.action==0)
        {
            //sprRend.sprite = bscAtk[];
        }
    }
}
