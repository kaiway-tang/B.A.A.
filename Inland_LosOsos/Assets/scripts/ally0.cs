using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ally0 : MonoBehaviour
{
    public Transform nmy;
    Transform player;
    int del;
    Rigidbody2D rb;
    public GameObject block;
    bool left;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = manager.playTrans;
        del = 470; //prevents action from this ally for a delay while the boss is talking
    }

    void FixedUpdate()
    {
        if (nmy.position.x > transform.position.x)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        if (del<1)
        {
            if (transform.position.x > player.position.x + 7) //tests if ally is further than a certain distance from the player
            {
                left = true;
            }
            else
            if (transform.position.x < player.position.x - 7)
            {
                left = false;
            }else
            {
                int x = Random.Range(0,2);
                if (x==0)
                {
                    left = true;
                }
                else
                {
                    left = false;
                }
            }
            del = Random.Range(25,100);
        }
        if (del < 110)
        {
            if (left)
            {
                rb.velocity = new Vector2(-3, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(3, rb.velocity.y);
            }
        }
        del--;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="nmyAtk")
        {
            Instantiate(block, col.transform.position, transform.rotation);
            Destroy(col.gameObject);
        }
    }
}
