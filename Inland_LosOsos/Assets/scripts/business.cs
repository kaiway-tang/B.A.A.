using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class business : MonoBehaviour
{
    public Color black;
    public static float hp;//the business bosses share one health bar
    public GameObject[] projectile; //bullets that this boss shoots
    public Sprite[] sprites; //for animations
    public int spd;
    public Transform hand; //hand's position where projectiles will spawn from
    public bool left;
    int del;
    int atk; //timer for casting attacks
    int walk; //timer for walking animation
    public baseNmyScript baseScript; //the base script for enemies that manages hp, knockback, etc
    public Transform other; //the other business boss
    public Transform hpBar;
    public GameObject[] cleanUp;
    public GameObject[] pivots;
    public GameObject leader;
    public GameObject palmFX;
    int death;
    int intro;
    public GameObject[] text;

    SpriteRenderer sprRend;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        hp = 800;
        sprRend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        intro = 240;
    }

    void FixedUpdate()
    {
        if (intro > 0) //plays the intro text of the boss
        {
            if (intro == 210)
            {
                text[0].SetActive(true);
            }
            if (intro == 140)
            {
                text[0].SetActive(false);
                text[1].SetActive(true);
            }
            if (intro == 70)
            {
                text[1].SetActive(false);
                text[2].SetActive(true);
            }
            if (intro==1)
            {
                text[2].SetActive(false);
                baseScript.enabled = true; //enables the script controlling the health of the boss which was previously disabled so that it couldnt take damage while it played its intro dialogue
                rb.gravityScale = 10;
                GetComponent<BoxCollider2D>().enabled = true;
            }
            intro--;
        }
        else
        {
            if (death > 0)
            {
                death--;
                if (death == 1)
                {
                    Instantiate(cleanUp[2], transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
                    leader.SetActive(true);
                    Destroy(gameObject);
                }
            }
            hpBar.transform.position = new Vector3((transform.position.x + other.position.x) / 2, 4, 0);
            //keeps the business hp bar in the middle of the two business bosses
            if (hp <= 0 && death < 1)
            { //runs if the business boss is slain (0 hp)
                leader.transform.position = cleanUp[0].transform.position;
                //moves the leader boss so that it spawn in between the two business bosses
                Destroy(cleanUp[0]); //destroys the visible hp bar
                Destroy(cleanUp[1]); //destroys the health lines
                sprRend.color = black; //blackens the tuxedo making it appear shriveled and dead
                death = 50;
                baseScript.enabled = false;
            }
            if (baseScript.disabled < 1)
            {
                if (del < 1)
                {
                    if (transform.position.x > other.position.x + 10)
                    {
                        left = true;
                        transform.localEulerAngles = new Vector3(0, 180, 0); //reflects the enemy to face the other way
                    }
                    else //ensures the bosses dont separate too far
                    if (transform.position.x < other.position.x - 10)
                    {
                        left = false;
                        transform.localEulerAngles = Vector3.zero; //reflects the enemy to face the other way
                    }
                    else
                    {
                        int x = Random.Range(0, 3);
                        //the business boss has a 33% chance of turning to face the player every few seconds
                        if (x < 2)
                        {
                            if (left)
                            {
                                left = false; transform.localEulerAngles = Vector3.zero; //reflects the enemy to face the other way
                            }
                            else
                            {
                                left = true;
                                transform.localEulerAngles = new Vector3(0, 180, 0); //reflects the enemy to face the other way
                            }
                        }
                        else
                        {
                            if (manager.playTrans.position.x > transform.position.x)
                            {
                                left = false;
                                transform.localEulerAngles = Vector3.zero; //reflects the enemy to face the other way
                            }
                            else
                            {
                                left = true;
                                transform.localEulerAngles = new Vector3(0, 180, 0); //reflects the enemy to face the other way
                            }
                        }
                    }
                    del = Random.Range(100, 250);
                }
                else
                {
                    del--;
                }
                if (atk > 79) //boss stops moving when about to attack
                {
                    if (left)
                    {
                        rb.velocity = new Vector3(-spd, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector3(spd, rb.velocity.y);
                    }
                    if (walk < 1) { walk = 20; sprRend.sprite = sprites[0]; }
                    else
                    {
                        walk--;
                        if (walk == 10) { sprRend.sprite = sprites[2]; }
                    }
                    //plays the bosses walking animation
                }
                if (atk > 0)
                {
                    if (atk == 80)
                    {
                        palmFX.SetActive(true);
                    }
                    atk--;
                    if (atk == 25)
                    {
                        palmFX.SetActive(false);
                        sprRend.sprite = sprites[1]; //sets the sprite of the boss to hold his hand out (indicates its about to attack)
                        int x = Random.Range(0, 2);
                        Instantiate(projectile[x], hand.transform.position, transform.rotation);
                        //summons the projectile of the selected attack
                    }
                    if (atk == 1)
                    {
                        sprRend.sprite = sprites[0];
                        //sets the sprite back to the normal idle sprite when it finishes attacking
                    }
                }
                else
                {
                    atk = Random.Range(120, 200);
                    //sets a random cooldown after the boss attacks
                }
            }
        }
    }
}
