using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bscNmy : MonoBehaviour
{
    public float spd; //speed at which enemy moves
    bool left; //determines whether to move left or right
    int del; //delay for randomly swtiching movement direction
    int anim; //timer for animation
    public Sprite[] sprites;
    public SpriteRenderer sprRend;
    Rigidbody2D rb;

    baseNmyScript baseScript;

    void Start()
    {
        baseScript = GetComponent<baseNmyScript>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (anim>0)
        {
            if (anim==50) { sprRend.sprite = sprites[0]; }
            if (anim == 25) { sprRend.sprite = sprites[1]; }
            anim--;
        } else { anim = 50; }
        if (baseScript.disabled < 1)
        {
            del--;
            if (del < 1)
            {
                if (left)
                {
                    left = false; transform.localScale = new Vector3(1,1,1); //reflects the enemy to face the other way
                }
                else { left = true;
                    transform.localScale = new Vector3(-1, 1, 1); //reflects the enemy to face the other way
                }
                del = Random.Range(1, 200);
            }
            if (left)
            {
                if (rb.velocity.x < 3) { rb.velocity = new Vector3(-spd, rb.velocity.y); }
            }
            else
            {
                if (rb.velocity.x < 3) { rb.velocity = new Vector3(spd, rb.velocity.y); }
            }
        }
    }

}
