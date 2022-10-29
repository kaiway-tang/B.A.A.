using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPoint : MonoBehaviour
{
    public Transform point;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (point)
        {
            Vector3 direction = transform.position - point.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
        }
    }
}
