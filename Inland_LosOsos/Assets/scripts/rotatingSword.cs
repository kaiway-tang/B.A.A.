using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingSword : MonoBehaviour
{
    public Sprite[] swords;
    public SpriteRenderer sprRend;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up*.5f);
        if (transform.localEulerAngles.x==90|| transform.localEulerAngles.x == -90) {
            if (sprRend.sprite==swords[0])
            {
                sprRend.sprite = swords[1];
            }
            else
            {
                sprRend.sprite = swords[0];
            }
        }
    }
}
