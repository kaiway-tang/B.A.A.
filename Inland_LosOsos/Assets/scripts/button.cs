using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public float distance;
    public Color[] colors;
    public int function; //-1: other; 1: enter Tweeter mode;
    public int[] ints;
    public GameObject space;
    public Transform plyr;
    bool onEnter;
    // Start is called before the first frame update
    void Start()
    {
        plyr = manager.playTrans;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(plyr.position,transform.position)<=distance)
        {
            if (!onEnter)
            {
                space.SetActive(true);
                onEnter = true;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (function==1)
                {
                    tweeterMode.ride = true;
                    Destroy(gameObject);
                }
                else if (function == 0)
                {

                }
                else if(function == 0)
                {

                }
                else if(function == 0)
                {

                }
            }
        } else
        {
            if (onEnter)
            {
                space.SetActive(false);
                onEnter = false;
            }
        }
    }
}
