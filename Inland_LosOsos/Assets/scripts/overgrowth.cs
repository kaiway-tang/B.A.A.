using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overgrowth : MonoBehaviour
{
    public GameObject vines;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "aegis" && Aegis.del > 0)
        {
            Instantiate(vines, transform.position+new Vector3(.2f,0,0), vines.transform.rotation);
            Destroy(gameObject);
        }
    }
}
