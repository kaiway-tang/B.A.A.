using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOF : MonoBehaviour
{
    public static int hp; //boss' hitpoints
    Transform plyr;
    public Transform hpScaler; //the objects that controls the visible hp bar
    public Transform hpScalerFX;
    public float hpFX;
    public GameObject minion; //the minion this boss spawns
    public Color dead; //the defeated color of the boss
    public GameObject smoke; //the smoke the destroyed robot emits
    public GameObject portal; //the portal that is enabled when the boss is defeated allowing player to proceed
    int del1; //timer for doing dead things like creating portal and falling
    int del; //minion spawning timer
    public GameObject blockUncover; //the particle effects that play when the portal is revealed
    bool once; //only runs certain code once
    int intro;
    int death;
    public GameObject[] text;
    public ParticleSystem[] thrusts;

    void Start()
    {
        hp = 10; hpFX = hp;
        plyr = manager.playTrans;
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
            if (intro == 1)
            {
                text[2].SetActive(false);
            }
            intro--;
        }
        else
        {
            if (death>0) { death--;
                if (death==100) { text[3].SetActive(true); }
                if (death == 1) { text[3].SetActive(false); } }
            //plays the death text of the boss
            if (hp >= 0)
            {
                hpScaler.transform.localScale = new Vector3(hp / 10f, 1, 1);
                if (hpFX>hp)
                {
                    hpFX-=0.05f;
                    hpScalerFX.transform.localScale = new Vector3(hpFX / 10f, 1, 1);
                }
                //adjusts the hp bar as the boss loses health
            }
            if (hp <= 0)
            {
                if (!once) //runs the following code only once
                {
                    thrusts[0].loop = false;
                    thrusts[1].loop = false; //deactivates the thrust effects when the boss is slain
                    GetComponent<SpriteRenderer>().color = dead;
                    GetComponent<Rigidbody2D>().gravityScale = 10; //makes the robot fall to the ground
                    smoke.SetActive(true); //sets active the smoke effects
                    del1 = 120;
                    once = true;
                    death = 140;
                }
            }
            if (del1 > 0)
            {
                del1--; if (del1 == 104) { Destroy(GetComponent<Rigidbody2D>()); } //destroys the physics component of the boss so that it stops falling and doesnt fall through the ground
                if (del1 == 1)
                {
                    Instantiate(blockUncover, portal.transform.position, transform.rotation);
                    portal.SetActive(true); //reveals the portal to advance to the next level
                }
            }
            if (hp > 0)
            {
                if (del < 50)
                {
                    if (del % 4 == 0)
                    {
                        transform.position -= new Vector3(0.2f, 0, 0);
                    }
                    else if (del % 2 == 0)
                    {
                        transform.position += new Vector3(0.2f, 0, 0);
                    }
                    //if del < 50, the boss prepares to spawn a minion and telegraphs by shaking left and right rapidly
                }
                else
                {
                    if (plyr.transform.position.x > transform.position.x)
                    {
                        transform.position += new Vector3(.05f, 0, 0);
                    }
                    else
                    {
                        transform.position -= new Vector3(.05f, 0, 0);
                    }
                    //follows the player left and right
                }
                if (del < 1)
                {
                    Instantiate(minion, transform.position, minion.transform.rotation);
                    if (hp < 6) { del = Random.Range(100, 150); } else { del = Random.Range(150, 250); }
                    //spawns minions much faster when it falls to half health by setting its cooldown to a lower value, otherwise sets it to a higher value
                }
                else
                { del--; }
            }
        }
    }
}
