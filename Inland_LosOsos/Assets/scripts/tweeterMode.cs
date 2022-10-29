using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tweeterMode : MonoBehaviour
{
    public Transform[] obstacles; //the obstacles the player needs to bypass
    public GameObject tweeter; //the tweeter bird object
    public Rigidbody2D playerRB; //players rigidbody to disable gravity
    public Rigidbody2D tweeterRB; //tweeter's rigidbody
    public player player; //disables the player script while riding tweeter
    public static bool ride; //true if tweeter ride has begun
    bool once; //corresponding code only runs once
    int del; //cooldown between tweeter flaps
    //int del0; //delay controlling staggered movement of obstacles
    public SpriteRenderer sprRend;
    public Sprite[] sprites;
    public GameObject border;
    public int loadNext;
    public Transform transitionCircle;
    public GameObject blackCircle;
    public AudioSource speaker;
    public AudioClip clip;
    GameObject pauseImg;

    void Start()
    {
        pauseImg = GetComponent<player>().pauseImg;
    }

    void Update()
    {
        if (ride) { player.Pause(pauseImg); }
    }

    void FixedUpdate()
    {
        if (ride)
        {
            if (!once) //runs the following code only once
            {
                //GetComponent<CapsuleCollider2D>().enabled = false;
                manager.Aegis.GetComponent<BoxCollider2D>().isTrigger = true;
                GetComponent<CapsuleCollider2D>().enabled = false; //prevents an unusual exploit where aegis can interact physically with tweeter
                tweeterRB.gravityScale = 4; //makes tweeter have gravity
                manager.mode = 2; //camera now has stable y coord
                transform.localScale = new Vector3(.7f, .7f, 1);
                transform.parent = tweeter.transform; //sets the parent of the game object to the bird
                playerRB.velocity = new Vector2(0,0);
                transform.position = tweeter.transform.position + new Vector3(-.35f,.35f,0); //moves player onto tweeter
                Destroy(playerRB);
                player.enabled = false;
                once = true;
                tweeter.transform.localEulerAngles = new Vector3(0,0,0); //makes the bird lay flat from its "standing" initial position
                border.SetActive(false); //disables the invisible wall that prevents the player from jumping off the edge
            }
            if (Input.GetKey(KeyCode.W) && del < 1)
            {
                del = 15;
                sprRend.sprite = sprites[1];
                tweeterRB.velocity = new Vector2(0, 10);//tweeter flaps and gains thrust and sets tweeter's sprite to the wings down flap sprite for .3 seconds
                speaker.Play();
            }
            if (del>0) { del--; if (del == 1) { sprRend.sprite = sprites[0]; } } //sets tweeter's sprite to the default wings up animation to suggest falling
            if (tweeter.transform.position.y<-9|| tweeter.transform.position.y > 4.5) //tests for if the tweeter bird is too high or too low and resets the level if so
            {
                finalScene.damageOnes++; //failing tweeter mode counts as taking damage when calculating the final hero score
                manager.reset = true; ride = false;
            }
            tweeter.transform.position += new Vector3(.09f,0,0); //move tweeter forward constantly
            for (int i = 0; i < 3; i++)
            {
                obstacles[i].transform.position += new Vector3(.05f, 0, 0); //move obstacles forward slightly slower than tweeter
            }
            if (tweeter.transform.position.x>188&&loadNext==0) {
                loadNext = 25; //delay allows for the blacking-out-screen scene transitions
                speaker.clip = clip;
                speaker.Play();
            }
            if (loadNext>0)
            {
                if (loadNext == 25) { blackCircle.SetActive(true); music.nextSong = true; }
                loadNext--;
                transitionCircle.localScale -= new Vector3(loadNext / 5, loadNext/ 5, 0);
                //creates the closing-in transition effect by shrinking a circle
                if (loadNext==1) { SceneManager.LoadScene("Business"); }
            }
        }
    }
}
