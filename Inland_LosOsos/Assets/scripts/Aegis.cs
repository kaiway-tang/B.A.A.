using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aegis : MonoBehaviour
{
    public static Transform self; //makes position accesible to other scripts
    Camera cam;
    public SpriteRenderer sprRend; //the spriterendere so that ths sprite can chnge between sword and shield
    public CapsuleCollider2D capsCol; //the collider to enable in sword mode
    public BoxCollider2D boxCol; //the collider to enable in shield mode, allows physical interaction
    public BoxCollider2D slamCol;
    public Sprite[] sprites; //0: sword; 1: shield
    // Start is called before the first frame update
    public static int del; //delay for dashing and determines sword/shield mode
    int cd; //cooldown for casting abilities
    public GameObject dash;
    public GameObject blockFX;
    bool left;
    bool right;
    public AudioClip[] sounds;
    AudioSource speaker;
    public static bool shieldSlam;

    void Start()
    {
        self = transform;
        transform.parent = null;
        cam = manager.cam;
        speaker = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!player.pause)
        {
            if (Input.GetMouseButtonDown(0)) { left = true; }
            if (Input.GetMouseButtonDown(1)) { right = true; }
        }
    }
    void FixedUpdate()
    {
        if (cd < 1)
        {
            if (left) //if left click
            {
                if (del < 0) //if in shield mode
                {
                    del = 0;
                    boxCol.enabled = false;
                    capsCol.enabled = true; //sets/disables colliders for sword/shield mode
                    manager.isShield = false; //informs other scripts that aegis is in sword mode
                    transform.localScale = new Vector3(.25f, .2f, 1);
                    sprRend.sprite = sprites[0];
                    gameObject.layer = 8;
                    speaker.clip = sounds[0];
                    speaker.Play();//plays the unsheathe sound when aegis transforms into a sword
                    faceMouse(100, transform);
                    dash.SetActive(true); //sets active the dash effect animation gameobject
                    del = 10;
                    cd = 25;
                }
                else
                {
                    speaker.clip = sounds[2];
                    speaker.Play();//plays the slash sound when aegis attacks

                    faceMouse(100, transform);
                    dash.SetActive(true); //sets active the dash effect animation gameobject
                    del = 10;
                    cd = 25;
                }
            }
            if (right&&!left)
            //if right click and not left click; prevents an unusual bug where left and right clicking at the same time causes the shield and dash sprite to simulataneously appear
            {
                if (del < 0)
                {
                    if (!shieldSlam)
                    {
                        del = 0;
                        boxCol.enabled = false;
                        capsCol.enabled = true;//sets/disables colliders for sword/shield mode
                        manager.isShield = false; //informs other scripts that aegis is in shield mode
                        transform.localScale = new Vector3(.25f, .2f, 1);
                        sprRend.sprite = sprites[0];
                        gameObject.layer = 8;
                        speaker.clip = sounds[0];
                        speaker.Play(); //plays the unsheathe sound when aegis transforms into a sword
                        cd = 25;
                    }
                    else if (cd < 1)
                    {
                        faceMouse(100, dash.transform);
                        del = 14;
                        cd = 25;
                        slamCol.enabled = true;
                        //manager.isShield = false; //allows aegis to deal damage with the shield slam attack
                    }
                }
                else
                {
                    capsCol.enabled = false;
                    boxCol.enabled = true;//sets/disables colliders for sword/shield mode
                    manager.isShield = true;//informs other scripts that aegis is in shield mode
                    sprRend.sprite = sprites[1];
                    transform.localScale = new Vector3(.25f, .25f, 1);
                    transform.localEulerAngles = Vector3.zero; //sets the shield rotation upright
                    del = -1;
                    gameObject.layer = 13;
                    speaker.clip = sounds[1];
                    speaker.Play(); //plays the clang sound when aegis transforms into a shield
                    cd = 5;
                }
            }
        }

        if (del==0) {
            transform.position += transform.up * .15f; //moves the sword forward slowly
            faceMouse(5,transform);
        }
        if (del>0)
        {
            if (del < 11)
            {
                if (del == 1) { dash.SetActive(false); }
                transform.position += transform.up * .7f; //moves the sword forward quickly
            }
            else
            {
                transform.position += dash.transform.up * .5f;
                if (del == 11) { del = 0; dash.transform.rotation = transform.rotation;
                manager.isShield = true; //once shield slam has ended, aegis no longer deals damage
                slamCol.enabled = false;
                }
            }
            del--;
        }
        if (cd>0) { cd--; }
        if (left) { left = false; }
        if (right) { right = false; }
    }
    void faceMouse(float turnSpd, Transform obj) //makes aegis face the mouse, turnSpd makes it snap to or turn slowly
    {
        Vector2 direction = transform.position - cam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        obj.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpd * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
        //if aegis is in shield mode and is hit by an enemy projectile, it blocks the projectile
    {
        if (col.gameObject.tag == "nmyAtk" && del == -1)
        {
            Instantiate(blockFX, col.transform.position, transform.rotation);
            Destroy(col.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "nmyAtk" && del == -1)
        {
            Instantiate(blockFX, col.transform.position, transform.rotation);
            Destroy(col.gameObject);
        }
    }
}
