using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class americaBoss : MonoBehaviour
{
    public float spd;
    public GameObject[] text;
    public int intro;
    public GameObject[] proj; //0: cannon bullets 1:air strike up 2: air strike down 3:fissure attack 4: hat minions
    public int atk;
    public int attack;
    public int attacking;
    int changeDir;
    bool left;
    Rigidbody2D rb;
    public GameObject hatImg;
    public GameObject crosshair;
    Transform playPos;
    int death;
    public baseNmyScript baseScript;
    public GameObject[] channels;
    int deathDel;
    public GameObject hpBar;
    public ParticleSystem deathFX;
    public Color black;
    public SpriteRenderer hatSprRend;
    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        playPos = manager.playTrans;
        rb = GetComponent<Rigidbody2D>();
        intro = -30;
    }

    void FixedUpdate()
    {
        if (intro < 310) //plays the intro dialogue of the boss
        {
            if (intro == 1)
            {
                text[0].SetActive(true);
            }
            else
            if (intro == 89)
            {
                text[0].SetActive(false);
                text[1].SetActive(true);
            }
            else if (intro == 199)
            {
                text[1].SetActive(false);
                text[7].SetActive(true);
            }
            else if (intro==309)
            {
                baseScript.enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
                rb.gravityScale = 10;
                //enables the script controlling the health of the boss which was previously disabled so that it couldnt take damage while it played its intro dialogue
                text[7].SetActive(false);
                text[2].SetActive(true);
            }
            intro++;
        }
        else
        {
            if (deathDel>0) //plays the death dialogue of the boss
            {
                if (deathDel==1950) {
                    GetComponent<BoxCollider2D>().enabled = false;
                    rb.gravityScale = 0;
                    rb.velocity = new Vector3(0, 0, 0);
                    //disables player interaction with the boss once defeated after a one second delay
                    //so if the boss is jumping when it dies it falls to the ground before freezing
                }
                    if (deathDel == 1800)
                {
                    text[3].SetActive(true);
                }
                if (deathDel == 1500)
                {
                    text[4].SetActive(true);
                    text[3].SetActive(false);
                }
                if (deathDel == 1200)
                {
                    text[5].SetActive(true);
                    text[4].SetActive(false);
                }
                if (deathDel == 900)
                {
                    text[6].SetActive(true);
                    text[5].SetActive(false);
                }
                if (deathDel == 600)
                {
                    text[6].SetActive(false);
                }
                if (deathDel<500)
                {
                    deathFX.emissionRate += 1; //makes more smoke over time
                    if (deathDel<400) //makes the hat fade away gradually
                    {
                        black.a-=0.01f;
                        hatSprRend.color = black;
                    }
                    if (deathDel==300) { deathFX.loop = false; } //ends the smoke effects to reveal the portal
                }
                if (deathDel==305) { portal.SetActive(true); }
                deathDel--;
            }
            if (baseScript.hp < 1)
            {
                if (deathDel < 1)
                {
                    rb.velocity = new Vector3(0, 0, 0);

                    hatSprRend.color = black; //colors the hat black to show it has been defeated
                    deathDel = 2000; //sets in motion the timer used to display the death text
                    hpBar.SetActive(false);
                    channels[0].SetActive(false);
                    channels[1].SetActive(false);
                    //in case the boss is using an attack when it dies, the channel is stopped
                    crosshair.SetActive(false);
                    //in case the boss is using its cannon attack (red attack), the crosshair is deactivated
                    manager.outs[1].SetActive(true);
                    //flashes the screen white
                    hatImg.transform.localEulerAngles = Vector3.zero;
                    //sets the rotation of the hat flat in case it is facing the player when it dies
                }
            }
            else
            {
                if (changeDir < 1)
                {
                    changeDir = Random.Range(45, 100);
                    int x = Random.Range(0, 2);
                    if (x == 0)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, 30, 0);
                    }
                    if (left)
                    {
                        left = false;
                    }
                    else
                    {
                        left = true;
                    }
                }
                if (attacking < 1&&baseScript.disabled<1) //moves the hat left and right only if it isnt performing an attack
                {
                    if (left)
                    {
                        rb.velocity = new Vector3(-spd, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector3(spd, rb.velocity.y);
                    }
                }
                changeDir--;
                if (atk > 0)
                {
                    atk--;
                }
                else
                {
                    if (attacking == 0) { attack = Random.Range(0, 3); } //randomly chooses an attack to perform
                    if (attack == 0)//do the crosshair rapid fire cannon attack
                    {
                        if (attacking == 0) { attacking = 100; channels[0].SetActive(true); }
                        if (attacking < 100)
                        {
                            if (attacking == 99) { crosshair.SetActive(true); }
                            Vector2 direction = transform.position - playPos.position;
                            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                            hatImg.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime); //makes the hat's bottom face the player
                            crosshair.transform.position = playPos.position; //makes the crosshair "target" the player
                            crosshair.transform.localEulerAngles += new Vector3(0,0,2);
                            if (attacking == 50) { channels[0].SetActive(false); }
                            if (attacking < 50 && attacking % 5 == 0) //tests if atk is divisible by 5, if so fires a bullet (in other words, runs the code every fifth tick)
                            {
                                Instantiate(proj[0], transform.position, hatImg.transform.rotation);
                            }
                            if (attacking == 1) { crosshair.SetActive(false); atk = Random.Range(50, 150); hatImg.transform.localEulerAngles = Vector3.zero; }
                            //once the attack is finished, sets the cooldown for the next attack between 1 and 3 seonds
                        }
                    }
                    if (attack == 1)
                    {
                        if (attacking == 0) { attacking = 150; hatImg.transform.localEulerAngles = new Vector3(0, 0, 0); }
                        if (attacking % 3 == 0) //runs the following code every third tick
                        {
                            if (attacking > 100) //for one second, shoots air strike bullets upwards telegraphing its attack
                            {
                                Instantiate(proj[1], transform.position + new Vector3(Random.Range(-.5f, .5f), 0, 0), transform.rotation); //shoots bullets upwards to "prepare" the air strike attack
                            }
                            else if (attacking < 50) //after two seconds, rains down bullets around the player's location
                            {
                                Instantiate(proj[2], playPos.position + new Vector3(Random.Range(-3, 4), 15, 0), transform.rotation); //makes the air strike bullets rain down around the player
                            }
                        }
                        if (attacking == 1) { atk = Random.Range(50, 150); hatImg.transform.localEulerAngles = Vector3.zero; }
                        //once the attack is finished, sets the cooldown for the next attack between 1 and 3 seonds
                    }
                    if (attack == 2)
                    {
                        if (attacking == 0)
                        {
                            attacking = 100;
                            channels[1].SetActive(true); //sets active the fissure channeling effect
                        }
                        changeDir++; //stops the changeDir timer from counting down so that the boss doesnt jump while channeling this attack
                        if (attacking > 10 && attacking < 70)
                        {
                            rb.velocity = new Vector2(0, 0.04f);
                            transform.position += new Vector3(0, 0.1f, 0);//makes the hat float upwards
                        }
                        if (attacking == 1)
                        {
                            channels[1].SetActive(false);
                            atk = Random.Range(50, 150); Instantiate(proj[3], transform.position, hatImg.transform.rotation);
                            GameObject fissure = Instantiate(proj[3], transform.position, hatImg.transform.rotation);
                            fissure.transform.localEulerAngles += new Vector3(0, 180, 0); //spins the second fissure summoning bullet around to travel the other way
                        }
                    }
                    if (attack == 3) //attack under development
                    {
                        if (attacking == 0) { attacking = 100; }
                    }
                }
                if (attacking > 0) { attacking--; }
            }
        }
    }
}
