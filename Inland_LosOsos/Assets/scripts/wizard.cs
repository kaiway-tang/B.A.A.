using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizard : MonoBehaviour
{
    public player playScript;
    Transform player;
    Rigidbody2D rb;
    int del;
    public GameObject heal;
    bool left;
    public hitbox hitbox;
    public static int maxHP;

    void Start()
    {
        player = manager.playTrans;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (del > 0) { del--; } else
        {
            if (playScript.hp<maxHP)
            {
                del = 1800;
                GameObject Heal= Instantiate(heal, transform.position, transform.rotation);
                Heal.GetComponent<heal>().hitbox = hitbox;
            }
        }
        if (transform.position.x > player.position.x + 2) //tests if ally is further than a certain distance from the player
        {
            if (!left) { transform.localScale = new Vector3(-.28f, .28f, 1); left = true; }
            rb.velocity = new Vector2(-3, rb.velocity.y);
        }
        else
            if (transform.position.x < player.position.x - 2)
        {
            if (left) { transform.localScale = new Vector3(.28f, .28f,1); left = false; }
            rb.velocity = new Vector2(3, rb.velocity.y);
        }
    }
}
