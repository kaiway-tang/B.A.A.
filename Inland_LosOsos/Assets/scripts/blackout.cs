using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackout : MonoBehaviour
{
    public int outNum;
    public SpriteRenderer sprRend;
    public int spd;
    int del;
    public Color fade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (outNum==0)
        {
            fade.a += 0.01f;
            sprRend.color = fade;
        }else if(outNum==1)
        {
            fade.a -= 0.01f;
            sprRend.color = fade;
        }
        else if (outNum == 2)
        {
            fade.a -= 0.06f;
            sprRend.color = fade;
            if (fade.a<=0) { fade.a = .608f;sprRend.color = fade; gameObject.SetActive(false); }
        }
    }
}
