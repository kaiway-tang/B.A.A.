using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public int hp;
    public static int BP;
    public static int baseDmg;
    public int jump;
    int[] bscAtk; //0:atkPhase 1:resetTimer
    bool right; //true if player facing right (D key)

    public static int action;//0-2:bscAtk 3:idle 4:run
    public GameObject[] bscAtkHB; //bsc atk hitboxes
    public GameObject[] swipes;
    public Transform sword;
    int stepFwd;
    int stop;
    public Transform transitionCircle;
    public GameObject blackCircle;

    Rigidbody2D rb;
    public static int del;
    public static int start;
    public static int end;
    public int disable;
    public GameObject[] outs;
    public static bool pause;
    public GameObject pauseImg;
    public SpriteRenderer swordSprRend;
    public Sprite[] swords;
    public AudioSource[] sounds;
    public hitbox hitbox;
    public static bool extraHealth;
    public static bool sprint;
    public int[] doubleTap; //0: A double tap timer 1: D double tap timer

    void Start()
    {
        pauseImg.transform.parent = manager.camTrans; //makes the pause screen follow the camera
        for (int i = 0; i < 3; i++)
        //sends an array containing the blackout, whiteout, and redout screen effects to the manager script where they are accesible by other scripts
        //doing this allows the array to be modified in the inspector and allows modification of every scene through a single player prefab
        {
            manager.outs[i] = outs[i];
            outs[i].transform.parent = manager.camTrans;
            outs[i].transform.position = manager.camTrans.position + new Vector3(0, 0, 10);
        }
        right = true; //the player faces right by default when beginning any level
        baseDmg = 30; //the base damage of the player, attacks deal this amount of damage modified by a multiplier

        bscAtk = new int[2]; //a set of variables controlling the player's combo attack ability
        if (extraHealth) { hp = 5; } else { hp = 4; hitbox.hearts[4].SetActive(false); }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)&&jump>0&&disable<1)
        //jumps when W is pressed, the player has not expended their double jumps, and they are not under the brief incapacitation from taking damage
        {
            rb.velocity = new Vector2(rb.velocity.x, 22);
            jump -= 1; //determines how many times the player can jump
        }
        Pause(pauseImg);
        if (sprint)
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                doubleTap[0] = 15;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                doubleTap[1] = 15;
            }
        }
    }
    void FixedUpdate()
    {
        /*if (start<25)
        {
            start++;
            transitionCircle.localScale += new Vector3(start,start,0);
        }*/
        if (end>0)
        {
            if (end == 25) { blackCircle.SetActive(true); }
            end--;
            transitionCircle.localScale -= new Vector3(end/5, end/5, 0);
            //creates the closing-in transition effect by shrinking a circle
        }
        if (del > 0) {
            if (del > 1)
            {
                if (action == 0)
                {
                    sword.transform.localEulerAngles += new Vector3(0, 0, -20f);
                    //swings the sword around in a crescent
                }
                if (action==1)
                {
                    sword.transform.localEulerAngles += new Vector3(0, 0, -30f);
                    //swings the sword a full circle around the player
                }
            } else
            {
                sword.transform.localEulerAngles = new Vector3(0, 0, -12);
                sword.transform.localPosition = Vector3.zero;
                sword.transform.localScale = new Vector3(1, 1, 1);
                //resets the swords position, rotation, and scaling
                for (int i = 0; i < 3; i++)
                {
                    swipes[i].SetActive(false); //deactivates the three swipe animations
                }
            }
            del --;
        }
        if (stop>0) { stop--;if (stop == 1) { rb.velocity = new Vector2(0, 0); } }
        atk();
        if (disable > 0) { disable--; }
        movement();
        if (doubleTap[0]>0) { doubleTap[0]--; }
        if (doubleTap[1] > 0) { doubleTap[1]--; }
    }
    void atk()
    {
        if (Input.GetKey(KeyCode.P)&&bscAtk[1]<25)
        {
            swordSprRend.sprite = swords[1]; //sets the swords sprite to a charged glow effect when attacking
            if (bscAtk[0] < 3)
            {
                action = bscAtk[0]; bscAtkHB[bscAtk[0]].SetActive(true);
                swipes[action].SetActive(true); //sets active the swipe animation corresponding to the attack
                if (action==0)
                {
                    sword.transform.localEulerAngles = new Vector3(0, 0, 30);
                    del = 9;
                    stepFwd = 3;
                }
                if (action==1)
                {
                    sword.transform.localEulerAngles = new Vector3(0, 0, -15);
                    del = 14;
                    stepFwd = 3;
                }
                if (action == 2)
                {
                    sword.transform.localEulerAngles = new Vector3(0, 0, -80);
                    sword.transform.localPosition = new Vector3(0,0.38f,0);
                    sword.transform.localScale = new Vector3(1,1.2f,1);
                    del = 8;
                    stepFwd = 50;
                    stop = 5;
                }
                bscAtk[0]++;
                bscAtk[1] = 40;
            }
            if (right)
            {
                rb.velocity = new Vector2(stepFwd, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-stepFwd, rb.velocity.y);
            }
        }
        if (bscAtk[1]==25) { bscAtkHB[bscAtk[0]-1].SetActive(false);
        }
        if (bscAtk[1]==0) { bscAtk[0] = 0; swordSprRend.sprite = swords[0]; //sets the swords sprite to a normal discharged state while not attacking
        }
            bscAtk[1]--;
    }
    void movement()
    {
        if (bscAtk[1] < 25)
        {
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)&&disable<1)
            {
                if (doubleTap[0] > 0)
                {
                    doubleTap[0] = 2;
                    right = false;
                    transform.localScale = new Vector3(-.7f, .7f, 1);
                    rb.velocity = new Vector2(-11, rb.velocity.y);
                }
                else
                {
                    right = false;
                    transform.localScale = new Vector3(-.7f, .7f, 1);
                    rb.velocity = new Vector2(-8, rb.velocity.y);
                }
            }
            else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && disable < 1)
            {
                if (doubleTap[1] > 0)
                {
                    doubleTap[1] = 2;
                    right = true;
                    transform.localScale = new Vector3(.7f, .7f, 1);
                    rb.velocity = new Vector2(11, rb.velocity.y);
                }
                else
                {
                    right = true;
                    transform.localScale = new Vector3(.7f, .7f, 1);
                    rb.velocity = new Vector2(8, rb.velocity.y);
                }
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag=="aegis") { jump = 2; } //if the player lands on aegis, allows it to jump
    }
    public static void Pause(GameObject pauseImage)
    //allows the tweeter mode script to pause the game since the player script is disabled in tweeter mode
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                Time.timeScale = 1f;//resumes the game by setting the time scale to 1
                pause = false;
                pauseImage.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f; //pauses the game by setting the time scale to 0
                pause = true;
                pauseImage.SetActive(true);
            }
        }
        if (pause)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("firstScene");
                //quits by returning the player to the start screen where they can close the application and sets the time scale back to 1
            }
        }
    }
}
