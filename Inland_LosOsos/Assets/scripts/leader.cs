using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leader : MonoBehaviour
{
    public float spd;
    public SpriteRenderer sprRend;
    public Transform hand;
    public GameObject[] projectile;
    public Transform[] pivots;
    public Sprite[] sprites; //for animations
    public int atk; //timer for casting attacks
    int walk; //timer for walking animation
    bool left;
    public baseNmyScript baseScript;
    int del;
    public int x;
    Rigidbody2D rb;
    public int intro;
    public GameObject[] text;
    public GameObject deathFX;
    public Color fade;
    public int death;
    public GameObject palmFX;
    public GameObject bodyFX;
    public ParticleSystem[] pillars;
    public GameObject hpBar;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        intro = 260;
    }

    void FixedUpdate()
    {
        if (death > 0)
        {
            if (death == 1)
            {
                text[4].SetActive(true);
                Destroy(gameObject);
                pivots[0].transform.localEulerAngles = new Vector3(0, 0, -70);
                pivots[1].transform.localEulerAngles = new Vector3(0, 0, 70); //opens the hole in the ground revealing the portal}
            }
            if (death == 100)
            {
                text[3].SetActive(false);
                Instantiate(deathFX, transform.position, transform.rotation);
            }
            if (death < 100)
            {
                sprRend.color = fade;
                fade.a -= 0.01f;
            }
            death--;
        }
        if (intro > 0) //plays the intro text of the boss
        {
            intro--;
            if (intro == 1)
            {
                baseScript.enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
                rb.gravityScale = 10;
                //this script is disabled beforehand so that the boss of leader cant be attacked while hes "talking"
                text[2].SetActive(false);
            }
            if (intro == 70)
            {
                text[2].SetActive(true);
                text[1].SetActive(false);
            }
            if (intro == 140)
            {
                text[1].SetActive(true);
                text[0].SetActive(false);
            }
            if (intro == 210)
            {
                text[0].SetActive(true);
            }
            if (intro==236)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                rb.gravityScale = 0;
            }
        }
        else
        {
            if (baseScript.hp < 1 && death == 0)
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector3(0, 0, 0);
                hpBar.SetActive(false);
                GetComponent<BoxCollider2D>().enabled = false;
                //prevents the player from interacting with the defeated boss

                death = 150;
                Destroy(baseScript.hpBar);
                text[3].SetActive(true);
                baseScript.enabled = false;
                transform.localEulerAngles = Vector3.zero;
            }
            if (baseScript.disabled < 1)
            {
                if (del < 1)
                {
                    int x = Random.Range(0, 3);
                    //the leader boss has a 66% chance of turning to face the player every few seconds
                    if (x == 0)
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
                    del = Random.Range(50, 100);
                    //sets a random cooldown for the boss after it attacks between 1 and 2 seconds
                }
                else
                {
                    del--;
                }
                if (atk > 69)
                //makes the boss move left and right unless atk<70, during which it stands still as it gets ready to attack
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
                        if (walk == 10) { sprRend.sprite = sprites[1]; }
                    }
                    //plays the bosses walking animation
                }
                if (atk > 0)
                {
                    atk--;
                    if (atk == 70)
                    {
                        x = Random.Range(0, 4);//randomly chooses an attack to perform
                        if (x != 3)
                        {
                            if (Mathf.Abs(manager.playTrans.position.x - transform.position.x) < 3)
                            {
                                x = Random.Range(1, 4);
                                if (x > 2)
                                {
                                    x = 3;
                                }
                                else
                                {
                                    palmFX.SetActive(true);//sets active the palm channeling effect
                                }
                                //if the player is "hugging" the boss, increases the chance that the boss will perform the melee pillar attack
                            }
                            else
                            {
                                palmFX.SetActive(true);//sets active the palm channeling effect
                            }
                        }
                    }
                    if (atk == 60 && x == 3)
                    {
                        bodyFX.SetActive(true);
                    }
                    if (x == 0)
                    {
                        if (atk == 40) { Instantiate(projectile[x], hand.transform.position, hand.transform.rotation); }
                        if (atk == 20) { Instantiate(projectile[x], hand.transform.position, hand.transform.rotation); }
                        //shoots two coin shot guns 20 ticks (.4 secs) apart
                    }
                    else if (x == 1)
                    {
                        if (atk == 40) { Instantiate(projectile[x], hand.transform.position, hand.transform.rotation); }
                        if (atk == 30) { Instantiate(projectile[x], hand.transform.position, hand.transform.rotation); }
                        if (atk == 20) { Instantiate(projectile[x], hand.transform.position, hand.transform.rotation); }
                        //shoots three dollar attacks, 10 ticks (.2 secs) apart
                    }
                    else if (x == 2)
                    {
                        if (atk == 40) { Instantiate(projectile[x], hand.transform.position, hand.transform.rotation); }
                        //shoots a single homing money bag attack
                    }
                    if (x == 3)
                    {
                        if (atk == 20)
                        {
                            projectile[3].SetActive(true);
                            bodyFX.SetActive(false);
                            pillars[0].Play(true);
                            pillars[1].Play(true);
                            pillars[0].loop = true;
                            pillars[1].loop = true;
                            //plays the pillar shooting up particle effect
                        }
                        if (atk == 1)
                        {
                            projectile[3].SetActive(false);
                            pillars[0].loop = false;
                            pillars[1].loop = false;
                            //disables the pillar shooting up particle effect
                        }
                    }
                    if (atk == 70&&x!=3)
                    {
                        sprRend.sprite = sprites[2];
                        //sets the sprite of the boss to hold his hand out (indicates its about to attack)
                    }
                    if (atk == 1)
                    {
                        sprRend.sprite = sprites[0];
                        palmFX.SetActive(false);
                    }
                }
                else
                {
                    atk = Random.Range(80, 130);
                }
            }
        }
    }
}
