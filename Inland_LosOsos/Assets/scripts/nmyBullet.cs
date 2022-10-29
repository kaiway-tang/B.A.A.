using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nmyBullet : MonoBehaviour
{
    bool every2; //runs certain code once every other tick to save some resources
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.up * .15f; //moves the bullet forward
        if (every2)
        {
            if (Vector3.Distance(transform.position,manager.bof.position)<.3f) { BOF.hp -= 1; Destroy(gameObject); }
            Vector3 direction = transform.position - manager.bof.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime); //rotates the bullet to face the boss of future
            every2 = false;
        } else
        {
            every2 = true;
        }
    }
}
