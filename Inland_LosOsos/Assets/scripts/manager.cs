using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    public static int mode; //tracking mode// 1:player only; 0: 1/2 way between aegis and player; 2: tweeter mode, stable y coord
    public Transform aegis;
    public GameObject playerObj;
    public Transform playerTrans;
    public Transform camPoint;
    public Transform boff;
    public static Transform Aegis;
    public static Transform bof; //these are for other scripts to reference more easily w/o get component, references boss of future position
    public static GameObject playObj; //these are for other scripts to reference more easily w/o get component
    public static Transform playTrans; //these are for other scripts to reference more easily w/o get component
    public static Camera cam; //for other scripts to use mouse position function
    public static bool reset; //true to reset level
    public Color black; //the transitioning color of the blacking out screen
    public static GameObject[] outs; //the gameobjects that covers the screen
    //0:black 1:white 2: red
    int resetting; //delay for blackout effect;
    public static int recruits;
    public static bool isShield;
    public static Transform camTrans;
    int time;
    public static float move;
    bool every2;
    float oldPos;
    // Start is called before the first frame update
    void Awake()
    {

        outs = new GameObject[3];
        mode = 0;
        Aegis = aegis; //makes aegis' position available to other scripts
        cam = GetComponent<Camera>();
        playObj = playerObj;
        playTrans = playerTrans;
        if (boff) { bof = boff; }
        isShield = false;
        camTrans = transform;
        transform.position = playTrans.position + new Vector3(0,0,-10);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Return)) { SceneManager.LoadScene("finalScene"); }
        time++;
        if (time==50) //a timer that tracks how long the player played the game for
        {
            finalScene.secOnes++;
            time = 0;
        }
        if (mode==0)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10); //gives the position of the mouse in a (x,y) coordinate
            float x = transform.position.x - (mousePos.x + playerTrans.position.x) / 2; //finds the distance between the camera and the mid point between the x coordinates of the mouse and the player
            float y = transform.position.y - (mousePos.y + playerTrans.position.y) / 2-2; //finds the distance between the mid point between the y coordinates of the mouse and the player
            transform.position = new Vector3(transform.position.x - x / 25, transform.position.y - y / 25, -10);
            //moves the camera 1/25 of the distance between its current position and the mid point between the mouse and the player
        }
        else if (mode == 1)  //to be used for single player mode (under development for nationals (if we qualify))
        {
            float x = transform.position.x - camPoint.position.x;
            float y = transform.position.y - camPoint.position.y;
            transform.position = new Vector3(transform.position.x - x / 15, transform.position.y - y / 15, -10);
            //moves the camera 1/15 of the distance between its current position and the camPoint position
        }
        else if (mode==2) //used for tweeter mode
        {
            float x = transform.position.x - (aegis.position.x + playerTrans.position.x) / 2; //finds the distance between the camera and the mid point between the x coordinates of aegis and the player
            float y = transform.position.y +2; //gives a fixed y coordinate 
            transform.position = new Vector3(transform.position.x - x / 15, transform.position.y -y / 15, -10);
            //moves the camera 1/25 of the distance between its current position and the mid point between the aegis and the player
        }
        if (reset && resetting == 0)
        {
            resetting = 100;
            reset = false;
            outs[0].SetActive(true); //blacks out the screen with a fade effect
        }
        if (resetting>0) {
            if (resetting==1) {
                string currentScene = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentScene); }
            resetting--; }
        if (every2) //runs the following code only every other tick mainly to save resources
        {
            move = oldPos-transform.position.x; //finds the distance the camera moved in the last two ticks
            every2 = false;
        } else
        {
            oldPos = transform.position.x;
            every2 = true;
        }
    }
}
