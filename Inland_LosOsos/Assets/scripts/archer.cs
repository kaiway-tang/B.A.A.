using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archer : MonoBehaviour
{
    public GameObject arrow;
    public Transform nmy;
    int del;
    public int shootDel;
    Rigidbody2D rb;
    public baseNmyScript nmyBaseScript;
    bool left;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = manager.playTrans;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (nmyBaseScript.hp > 0)
        {
            if (nmy.position.x > transform.position.x)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            if (del < 1)
            {
                if (transform.position.x > player.position.x + 7) //tests if ally is further than a certain distance from the player
                {
                    left = true;
                }
                else
                if (transform.position.x < player.position.x - 7)
                {
                    left = false;
                }
                else
                {
                    int x = Random.Range(0, 2);
                    if (x == 0)
                    {
                        left = true;
                    }
                    else
                    {
                        left = false;
                    }
                }
                del = Random.Range(25, 100);
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
            if (shootDel > 0) { shootDel--; }
            else
            {
                int x = Random.Range(0, 3);
                Vector2 direction = transform.position - nmy.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
                GameObject Arrow = Instantiate(arrow, transform.position, Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime));
                if (x == 2) //33% chance that archer ally will shoot a trio of arrows
                {
                    Quaternion rotation1 = Quaternion.AngleAxis(angle + 105, Vector3.forward); //turns the arrow slightly upwards
                    Instantiate(arrow, transform.position, Quaternion.Slerp(transform.rotation, rotation1, 100 * Time.deltaTime));
                    Quaternion rotation2 = Quaternion.AngleAxis(angle + 75, Vector3.forward); //turns the arrow slightly downwards
                    Instantiate(arrow, transform.position, Quaternion.Slerp(transform.rotation, rotation2, 100 * Time.deltaTime));
                }
                shootDel = Random.Range(150, 200);
            }
        }
    }
}
