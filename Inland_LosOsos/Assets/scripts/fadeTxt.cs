using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeTxt : MonoBehaviour
{
    public int del;
    public SpriteRenderer sprRend;
    public Color fade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (del<50)
        {
            fade.a += 0.02f;
            sprRend.color = fade;
        }
        if (del > 100)
        {
            fade.a -= 0.005f;
            sprRend.color = fade;
        }
        del++;
    }
}
